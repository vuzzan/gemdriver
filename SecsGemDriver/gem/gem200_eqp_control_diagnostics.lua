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
-- print (os.date ("%d/%m/%Y %H:%M:%S"))
--====================================================
-- 
--====================================================
function processS2F13(s,f,sys,sml2,msg)
    print("processS2F13 Begin");
    -- Get all status ecid from message
	count_ecid=msg["$.1"].value;
	local ecid = {}
	local ecid_value = {}
	local sml = [[S2F14 = S2F14_EQP_CONST_DATA
<L
]]
	for i=1,count_ecid do
		ecid[i]=msg["$.1."..i].value;
		--print("ecid["..i.."]="..ecid[i]);
		--Query to database to get the ecid
		ecid_value = loadECIDfromDB(ecid[i]);
		--print("ecid_value["..i.."]="..ecid_value.value);
        --local ecid_value = i;
        if (ecid_value==nil or ecid_value=="") then
			sml = sml.."    <L[0]>\n"
		else
			sml = sml.."    <U4[1] "..ecid_value["dv_unit"]..">\n"
            --sml = sml.."    <U4[1] "..ecid[i]..">\n"
		end
		-- end set data
	end
sml = sml..[[>.
]]
--print(sml)
	--
	print("Send Reply S2F14 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F13 End\n");
end

function processS2F15(s,f,sys,sml2,msg)
    print("processS2F15 Begin");
    -- Get all ECID from message S2F15
	count_ecid=msg["$.1"].value;
	for i=1,count_ecid do
		ecid         =msg["$.1."..i..".1"].value;
		ecid_value   =msg["$.1."..i..".2"].value;
        print("processS2F15 ecid="..ecid.." ecid_value="..ecid_value);
		--Query to database to get the ecid
        rc = saveECIDtoDB(ecid, ecid_value);
		-- end set data
	end
    -- Report
	local sml = [[S2F16 = S2F16_EQP_CONST_SEND_ACK
<b[1] 0>.
]]
	print("Send Reply S2F16 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F16 End\n");
end

function processS2F13(s,f,sys,sml2,msg)
    print("processS2F13 Begin");
    -- Get all status ecid from message
	count_ecid=msg["$.1"].value;
	local ecid = {}
	local ecid_value = {}
	local sml = [[S2F14 = S2F14_EQP_CONST_DATA
<L
]]
	for i=1,count_ecid do
		ecid[i]=msg["$.1."..i].value;
		--print("ecid["..i.."]="..ecid[i]);
		--Query to database to get the ecid
		ecid_value = loadECIDfromDB(ecid[i]);
		--print("ecid_value["..i.."]="..ecid_value.value);
        --local ecid_value = i;
        if (ecid_value==nil or ecid_value=="") then
			sml = sml.."    <L[0]>\n"
		else
			sml = sml.."    <U4[1] "..ecid_value["dv_unit"]..">\n"
            --sml = sml.."    <U4[1] "..ecid[i]..">\n"
		end
		-- end set data
	end
sml = sml..[[>.
]]
--print(sml)
	--
	print("Send Reply S2F14 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F13 End\n");
end
function processS2F29(s,f,sys,sml2,msg)
    print("processS2F29 Begin....");
    -- Get all status ecid from message
	count_ecid = msg["$.1"].value;
	local ecid = {}
	local ecid_value = {}
	local sml = [[S2F30 = S2F30_EQP_CONST_NAME_LISTED
<L
]]
    if count_ecid=="0" then
        -- Load all CEID
        local ecid_list = {}
        ecid_list = loadECIDfromDBAll();
        print_r(ecid_list);
        for key, ecid_v in pairs(ecid_list) do
            sml = sml.."    <L\n"
            sml = sml.."        <U2[1] "..key..">\n";
            sml = sml.."        <A '"..ecid_v.name.."'>\n";
            sml = sml.."        <F4[1] '"..ecid_v.dv_limitmin.."'>\n";
            sml = sml.."        <F4[1] '"..ecid_v.dv_limitmax.."'>\n";
            sml = sml.."        <F4[1] '"..ecid_v.dv_default.."'>\n";
            sml = sml.."        <A '"..ecid_v.dv_unit.."'>\n";
            sml = sml.."    >\n"        
        end
        -- end load all CEID
    else
        -- only load specific ECID
        for i=1,count_ecid do
            ceid_uuid = msg["$.1."..i];
            --Query to database to get the ecid
            ecid_value = loadECIDfromDB(ceid_uuid.value);
            --local ecid_value = i;
            if (ecid_value==nil or ecid_value=="") then
                sml = sml.."    <L\n"
                sml = sml.."        <"..ceid_uuid.type.."[1] "..ceid_uuid.value..">\n";
                sml = sml.."        <L>\n";
                sml = sml.."        <L>\n";
                sml = sml.."        <L>\n";
                sml = sml.."        <L>\n";
                sml = sml.."        <L>\n";
                sml = sml.."    >\n"
            else
                sml = sml.."    <L\n"
                sml = sml.."        <"..ceid_uuid.type.."[1] "..ceid_uuid.value..">\n";
                sml = sml.."        <A '"..ecid_value.name.."'>\n";
                sml = sml.."        <F4[1] '"..ecid_value.dv_limitmin.."'>\n";
                sml = sml.."        <F4[1] '"..ecid_value.dv_limitmax.."'>\n";
                sml = sml.."        <F4[1] '"..ecid_value.dv_default.."'>\n";
                sml = sml.."        <A '"..ecid_value.dv_unit.."'>\n";
                sml = sml.."    >\n"
            end
            -- end set data
        end
        -- end only load specific ECID
    end
sml = sml..[[>.
]]
--print(sml)
	--
	print("Send Reply with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F29 End\n");
end

function processS2F31(s,f,sys,sml2,msg)
    print("processS2F31 Begin");
    -- Report
	local sml = [[S2F32 = S2F32_ACK
<b[1] 0>.
]]
	print("Send Reply S2F32 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F31 End\n");
end

function processS2F33(s,f,sys,sml2,msg)
    print("processS2F33 Begin. Define report");
    -- begin parse report
	data_id     = msg["$.1.1"].value;
    --
    count_rptid = msg["$.1.2"].value;
    if count_rptid==0 then
    else
        -- Update data id
        for i=1,count_rptid do
            --
            rpt_uuid      = msg["$.1.2."..i..".1"].value;
            count_vid   = msg["$.1.2."..i..".2"].value;
            if( count_vid == 0 ) then
                -- L0 at report ID, means delete this report ID and all link variable link to
                deleteReportIDfromDB(rpt_uuid, false);
                --
            else
                -- Delete all link to variable, keep the report ID
                rpt_id = deleteReportIDfromDB(rpt_uuid, true);
                -- Make the new link
                for j=1,count_vid do
                    vid = msg["$.1.2."..i..".2."..j].value;
                    rc = createnewlinkReport2Variable(rpt_id, vid);
                    if (rc<0 ) then
                        print("Can not link the rpt_id="..rpt_id.." to vid "..vid );
                    end
                    --
                end -- end list vid
            end
            --
        end -- end for
    end
    -- end parse report
	local sml = [[S2F34 = S2F34_ACK
<b[1] 0>.
]]
	print("Send Reply S2F34 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F33 End\n");
end

function processS2F35(s,f,sys,sml2,msg)
    print("processS2F35 Begin. Link report");
    -- begin parse link report
	data_id     = msg["$.1.1"].value;
    --
    count_ceid = msg["$.1.2"].value;
    if count_ceid==0 then
    else
        -- Update ceid
        for i=1,count_ceid do
            --
            ceid_uuid   = msg["$.1.2."..i..".1"].value;
            count_rptid = msg["$.1.2."..i..".2"].value;
            if( count_rptid == 0 ) then
                -- L0 at CEID, means delete this link to CEID.
                deleteReportlinkToCEIDfromDB(ceid_uuid);
                --
            else
                -- Delete all link to variable, keep the report ID
                ceid = deleteReportlinkToCEIDfromDB(ceid_uuid);
                -- Make the new link
                for j=1,count_rptid do
                    rpt_uuid = msg["$.1.2."..i..".2."..j].value;
                    rc = createnewlinkCEID2Report(ceid, rpt_uuid);
                    if (rc<0 ) then
                        print("Can not link the rpt_uuid="..rpt_uuid.." to CEID "..ceid );
                    end
                    --
                end -- end list vid
            end
            --
        end -- end for
    end
    -- end parse report
	local sml = [[S2F36 = S2F36_ACK
<b[1] 0>.
]]
	print("Send Reply S2F36 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F35 End\n");
end

function processS2F37(s,f,sys,sml2,msg)
    print("processS2F37 Begin. Enable/Disable CEID");
    -- begin parse list CEID
	ceed = tonumber( msg["$.1.1"].value );
    --
    count_ceid = msg["$.1.2"].value;
    if count_ceid==0 then
        -- Disable/Enable all CEID
        setEnableDisableCEIDAll(ceed);
        -- 
    else
        -- Update ceid
        for i=1,count_ceid do
            --
            ceid_uuid   = msg["$.1.2."..i].value;
            rc = setEnableDisableCEID( ceid_uuid, ceed);
            if (rc<0 ) then
                print("Can not find the ceid_uuid="..ceid_uuid.." in system." );
            end
        end -- end for
    end
    -- end parse report
	local sml = [[S2F38 = S2F38_ACK
<b[1] 0>.
]]
	print("Send Reply S2F38 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F37 End\n");
end

function processS2F39(s,f,sys,sml2,msg)
    print("processS2F39 Begin.");
    -- end parse report
	local sml = [[S2F40 = S2F40_ACK
<b[1] 0>.
]]
	print("Send Reply S2F40 with system byte"..sys);
	SendMessageSML(sml, sys);
    print("processS2F39 End\n");
end

