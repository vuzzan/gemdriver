--[[===========================================================================
--
    Copyright (C) secsdriver.com - All Rights Reserved
    Unauthorized copying of this file, via any medium is strictly prohibited
    Proprietary and confidential.
    Written by Neo NghiaLuong <vuzzan@gmail.com>
        
    HISTORY
    Date                Version     Author      Comment
    9:54 AM 11/25/2017  1.0         Neo         Initialize
    
--===========================================================================]]

--====================================================
-- IMPORT UTILS
-- import the LUA utils lib.
--====================================================
luasql = require "luasql.mysql"
env = assert (luasql.mysql())
con = assert (env:connect("gem","root","ok"))

ControlID = -1
-- LIB FOR PARSE XML
dofile( "gem/utils.lua")
-- LIB for parse JSON 
local JSON = (loadfile "gem/JSON.lua")()

dofile( "gem/gem_mysql.lua")
dofile( "gem/gem_controler.lua")
dofile( "gem/gem_variable.lua")
dofile( "gem/gem200_eqp_status.lua")
dofile( "gem/gem200_eqp_control_diagnostics.lua")

function autoReply(s,f,sys,sml,msg)
    local sml = "S"..s.."F"..(f+1)..[[ = AUTOREPLY
    <L[0]>.
    ]]
    print("Send Auto Reply with system byte"..sys);
    SendMessageSML(sml, sys);
end
--


--====================================================
-- PROCESS MESSAGE FROM EQUIPMENT
--====================================================
--====================================================
-- EQPReceived
-- THIS FUNCTION WILL BE CALLED WHEN SecsDriver RECEIVED
-- SECS-II PRIMARY MESSAGE FROM EQUIPMENT.
-- 
-- INPUT: 
--    1. s: Stream
--    2. f: Function
--    3. sys: System byte. ( this used for input to
--        the API SendSecondaryMessage()
-- This is recevive message is defined in XSML file.
-- 
--====================================================
function EQPReceived(s,f,sys)
    --print("EQPReceived Begin -- ControlID="..ControlID);
end
--====================================================
-- 
--====================================================
--[=====[  
==========================================================================================================
-- When secsdriver receive messsage which not be defined in XSML file, it will call this function to process message
-- Parameter:
    s: message stream id
    f: message function id
    sys: system byte ( message id)
    sml: Secs message in text format
    msg: all secs-ii item message.
    We can use the index like Jquery to access to item value. Example:
RECV S1F3W
<L[7/1]             --> msg["$.1"] . msg["$1"].arrSize=7
    <U2[1/1] 1000>  --> msg["$.1.1"]
    <U2[1/1] 1001>  --> msg["$.1.2"]
    <U2[1/1] 1002>
    <U2[1/1] 1003>
    <U2[1/1] 1004>
    <U2[1/1] 1005>
    <U2[1/1] 1006>
>
    to access the 1st U1, can address: $>1.1. Example: 
    
    local item = msg["$.1.1]"].value; -- To the 1st U1 = 1000. item will be equal = 1000
    item = msg["$.1.1"]; -- To the 1st U1 = 1000. item will be equal = 1000
    If you want to know, how many item in the LIST 
    local listCount = msg["$.1"].arrSize      ->listCount = 7
    
--========================================================================================================
--]=====] 
function EQPReceivedMsg(s,f,sys,sml,header10byte, msg)
    print("EQPReceivedMsg Begin -- ControlID="..ControlID.."\nS"..s.."F"..f.." systembyte="..sys..header10byte.."\n"..sml);
    logger("Recv: S"..s.."F"..f.." sys="..sys..header10byte..sml);
    if msg ~= nil then
        for k,v in pairs(msg) do
			local value = string.gsub(v.value, "%\"", "")
			v.value = value
			print("LUA: "..k.."= type="..v.type.." arrSize="..v.arrSize.." value="..value) 
		end
    else
        print("EQPReceivedMsg have message no thing");
    end
 
    if (s==1 and f==1) then
        processS1F1(s,f,sys,sml,msg)
    elseif (s==1 and f==3) then
        processS1F3(s,f,sys,sml,msg)
    elseif (s==1 and f==11) then
        processS1F11(s,f,sys,sml,msg)
    elseif (s==1 and f==13) then
        processS1F13(s,f,sys,sml,msg)
    elseif (s==1 and f==15) then
        processS1F15(s,f,sys,sml,msg)
    elseif (s==1 and f==17) then
        processS1F17(s,f,sys,sml,msg)
    elseif (s==2 and f==13) then
        processS2F13(s,f,sys,sml,msg)
    elseif (s==2 and f==15) then
        processS2F15(s,f,sys,sml,msg)
    elseif (s==2 and f==29) then
        processS2F29(s,f,sys,sml,msg)
    elseif (s==2 and f==31) then
        processS2F31(s,f,sys,sml,msg)
    elseif (s==2 and f==33) then
        processS2F33(s,f,sys,sml,msg)
    elseif (s==2 and f==35) then
        processS2F35(s,f,sys,sml,msg)
    elseif (s==2 and f==37) then
        processS2F37(s,f,sys,sml,msg)
    elseif (s==2 and f==39) then
        processS2F39(s,f,sys,sml,msg)
    else
        autoReply(s,f,sys,sml,msg)
    end
    print("EQPReceivedMsg END -- ControlID="..ControlID);
end
function MESRequest(xmlRequest, ctlId)
    print("MESRequest End\n");
end
function MESRequestJSON(jsonRequest, ctlId)
    print("MESRequestJSON Begin ctlId="..ctlId.." currentControlId="..ControlID.."\n");
    if(ctlId~=ControlID) then
        print("If this is not the same Control ID, do not process...\n")
        print("MESRequestJSON END...")
        return
    end
    -- print("-RequestName="..requestname);
    print("-JSON="..jsonRequest);
    
    -- decoce JSON string to object table
    local jsonReq = JSON:decode(jsonRequest);
    if(jsonReq==nil) then
        HostReport ("{\"code\":1,\"text\":\"Error to parse JSON\"}")
        return
    end
    --print_rd(jsonReq)
    if (jsonReq.command=="set") then
        local rc = 0;
        for i,v in ipairs(jsonReq.data) do 
            --print("SET " .. v.vid)
            local rc = setVariableToDatabase(v.vid, v.value);
            if rc~=nil then
                jsonReq.data[i]["code"] = 0;
                jsonReq.data[i]["text"] = "SET OK";
            else
                jsonReq.data[i]["code"] = 1;
                jsonReq.data[i]["text"] = "SET FAIL";
                rc = 1
            end
        end
        --
        jsonReq.data["code"] = rc;
        jsonReq.data["text"] = "SET OK";
        if rc==1 then
            jsonReq.data["text"] = "SET FAIL";
        end
        --
        HostReport (JSON:encode_pretty(jsonReq))
        --
    elseif (jsonReq.command=="get") then
        print("MESRequestJSON GET COMMAND...")
        --
        local rc = 0;

        for i,v in ipairs(jsonReq.data) do 
            print("GET FOR EVENT FIRST" .. v.vid)
            local rc = getVariableFromDatabase(v.vid);
            if rc~=nil then
                jsonReq.data[i]["code"] = 0;
                jsonReq.data[i]["text"] = "GET OK";
                jsonReq.data[i]["dv_value"] = rc["dv_value"];
                jsonReq.data[i]["dv_name"] = rc["dv_name"];
                jsonReq.data[i]["dv_unit"] = rc["dv_unit"];
            else
                jsonReq.data[i]["code"] = 1;
                jsonReq.data[i]["text"] = "GET FAIL";
                rc = 1
            end
        end
        --
        jsonReq.data["code"] = rc;
        jsonReq.data["text"] = "SET OK";
        if rc==1 then
            jsonReq.data["text"] = "SET FAIL";
        end
        --
        --
        HostReport (JSON:encode_pretty(jsonReq))
        --
    elseif (jsonReq.command=="event") then
        --
        for i,v in ipairs(jsonReq.data) do 
            --print("SET " .. v.vid)
            local rc = setVariableToDatabase(v.vid, v.value);
            if rc~=nil then
                jsonReq.data[i]["code"] = 0;
                jsonReq.data[i]["text"] = "SET OK";
            else
                jsonReq.data[i]["code"] = 1;
                jsonReq.data[i]["text"] = "SET FAIL";
            end
        end
        -- Get event ID
        local notifyEventData = notifyEventID ( jsonReq.evid, jsonReq.evname )
        -- BUILD SECS MESSAGE S6F11
        if( notifyEventData == nil ) then
            jsonReq["code"] = "1";
            jsonReq["text"] = "Can not found event ID="..jsonReq.evid;
        else
            jsonReq["code"] = "0";
            jsonReq["text"] = "";
            jsonReq["result"] = notifyEventData;
            local s6f11_sml = buildS6f11( notifyEventData );
        end
        
        --
        HostReport (JSON:encode_pretty(jsonReq))
        --
    else
        HostReport ("{\"code\":2,\"text\":\"Not support function "..jsonReq.command.."\"}")
    end
    print("MESRequestJSON End\n");
end
print( "\n\nLUA SCRIPT START SUCESSFULL. "..EQP_NAME.."\n\n" )