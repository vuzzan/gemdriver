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
-- 
--====================================================
function processS1F1(s,f,sys,sml,msg)
    local sml = [[S1F2 = S1F2
<L[2]
]]
    sml = sml.."    <A '"..EQP_NAME.."'>\n"
    sml = sml.."    <A '"..SOFTREF.."'>\n"
sml = sml..[[>.
]]
	SendMessageSML(sml, sys);
end

function processS1F3(s,f,sys,sml2,msg)
    print("processS1F3 Begin");
    -- Get all status svid from message S1F3
    print("GET processS1F3");
    
	count_svid  =   msg["$.1"].value;
    print("GET processS1F3 count_svid="..count_svid);
	local svid = {}
	local svid_value = {}
	local sml = [[S1F4 = S1F4_STATUS_REQ OUTPUT
<L
]]
	for i=1,count_svid do
		--print("GET svid["..i.."]= Index=".."$.1."..i.."[0]");
		svid[i] =   msg["$.1."..i.."[0]"].value;
		--print("GET svid["..i.."]="..svid[i]);
		--Query to database to get the svid
		svid_value = loadSVIDfromDB(svid[i]);
		--print("svid_value["..i.."]="..svid_value.value);
        --local svid_value = i;
        if (svid_value==nil or svid_value=="") then
			sml = sml.."    <L[0]>\n"
		else
			sml = sml.."    <U4[1] "..svid_value["value"]..">\n"
		end
		-- end set data
	end
sml = sml..[[>.
]]
--print(sml)
	--
	print("Send Reply S1F4 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS1F3 End\n");
end

function processS1F11(s,f,sys,sml2,msg)
    print("processS1F11 Begin");
    -- Get all status svid from message S1F3
	count_svid=msg["$.1"].value;
	local svid = {}
	local svid_value = {}
	local sml = [[S1F12 = S1F12_STATUS_NAMELIST OUTPUT
<L
]]
	for i=1,count_svid do
		svid[i]=msg["$.1."..i.."[0]"].value;
		--print("svid["..i.."]="..svid[i]);
		--Query to database to get the svid
		svid_value = loadSVIDfromDB(svid[i]);
		--print("svid_value["..i.."]="..svid_value.value);
        --local svid_value = i;
        sml = sml.."    <L"
        if (svid_value==nil or svid_value=="") then
			sml = sml.."    <U2[1] "..svid[i]..">\n"
			sml = sml.."    <L[0]>\n"
			sml = sml.."    <L[0]>\n"
		else
			sml = sml.."    <U2[1] "..svid[i]..">\n"
			sml = sml.."    <A '"..svid_value["name"].."'>\n"
			sml = sml.."    <A '"..svid_value["dv_unit"].."'>\n"
		end
        sml = sml.."    >\n"
		-- end set data
	end
sml = sml..[[>.
]]
--print(sml)
	--
	print("Send Reply S1F4 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS1F11 End\n");
end
function processS1F13(s,f,sys,sml,msg)
    local sml = [[S1F14 = S1F14
<L[2]
    <b[1] 0>
    <l[2]
]]
sml = sml.."    <A '"..EQP_NAME.."'>\n"
sml = sml.."    <A '"..SOFTREF.."'>\n"

sml = sml..[[
    >
>.
]]
	
    print("Send Reply S1F4 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS1F13 End\n");
end
function processS1F15(s,f,sys,sml,msg)
    
    local sml = [[S1F16 = S1F16
    <b[1] 0>.
]]
	SendMessageSML(sml, sys);
end
function processS1F17(s,f,sys,sml,msg)
    
    local sml = [[S1F18 = S1F18
    <b[1] 0>.
]]
	SendMessageSML(sml, sys);
end


