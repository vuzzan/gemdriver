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

local DATA_ID = 1;
--=============================================================================
-- Load setVariableToDatabase
-- input id, value
--=============================================================================
function setVariableToDatabase(vid, value)
	print("BEGIN setVariableToDatabase Begin"..vid);
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_uuid='"..vid.."'";
	cur = con:execute(sqlStatement)
    -- Can not find SVID in DB
    local isFound = 1
	if cur==nil then
		isFound = 0
	end
    -- Can not find SVID in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		isFound = 0
	end
    -- 
    -- Can not found, do insert 
    if( isFound== 0) then 
        print("BEGIN setVariableToDatabase Begin"..vid.." NOT FOUND");
        -- begin insert new UUID
        --[====[
        sqlStatement = "insert into eqpdatavariable(eqp_id, dv_type, dv_uuid, dv_unit, dv_desc, dv_datatype, dv_value, dv_valuetext,sts) values ("..
            .."1,"..
            .."2,"..        -- 2: ECID TYPE
            .."'..ecid..',"..
            .."'',"..
            .."'',"..
            .."'..ecid..',"..
            .."'..ecid..',"..
            
        con:execute(sqlStatement)
        ]====]--
        
        -- end insert new UUID
    else
        -- Founnd SVID in table. Load value here
        local svid_value = {};
        -- Set data value
        --
        local dv_datatype = tonumber(row.dv_datatype);
        svid_value["dv_id"] = row.dv_id
        svid_value["type"] = dv_datatype
        svid_value["name"] = row.dv_name
        svid_value["uuid"] = row.dv_uuid
        svid_value["dv_unit"] = row.dv_unit
        -- sqlStatement prepare
        sqlStatement = "update eqpdatavariable set ";
        if(dv_datatype==4 or dv_datatype==5 or dv_datatype==6) then
            -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
            svid_value["value"] = value
            sqlStatement = sqlStatement.. " dv_valuetext='"..svid_value["value"].."' ";
        else
            -- if this is the numeric value, get from field dv_value
            svid_value["value"] = tonumber( value )
            if( svid_value["value"]==nil) then svid_value["value"]=0 end
            sqlStatement = sqlStatement.." dv_value='"..svid_value["value"].."' ";
        end
        sqlStatement = sqlStatement.." where dv_id="..svid_value["dv_id"].."";
        -- Call to db to udpate query
        print(sqlStatement)
        con:execute(sqlStatement)
        --
        -- end update
        return svid_value
    end
	print("END setVariableToDatabase");
	--return not nil val
	return nil
end


--=============================================================================
-- Load getVariableFromDatabase
-- input id
--=============================================================================
function getVariableFromDatabase(vid)
	print("BEGIN getVariableFromDatabase Begin"..vid);
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_uuid='"..vid.."'";
	cur = con:execute(sqlStatement)
    -- Can not find SVID in DB
    local isFound = 1
	if cur==nil then
		isFound = 0
	end
    -- Can not find SVID in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		isFound = 0
	end
    -- 
    -- Can not found, do insert 
    if( isFound== 0) then 
    else
        -- Founnd SVID in table. Load value here
        local svid_value = {};
        -- Set data value
        --
        --print_r( row );
        svid_value["dv_id"] = row.dv_id
        svid_value["dv_datatype"] = row.dv_datatype
        svid_value["dv_name"] = row.dv_name
        svid_value["dv_uuid"] = row.dv_uuid
        svid_value["dv_unit"] = row.dv_unit
        if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
            -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
            svid_value["dv_value"] = row.dv_valuetext
        else
            -- if this is the numeric value, get from field dv_value
            svid_value["dv_value"] = row.dv_value
        end
        --
        -- end update
        -- print_r( svid_value );
        return svid_value
    end
	print("END setVariableToDatabase");
	--return not nil val
	return nil
end

--=============================================================================
-- Load notifyEventID
-- input evid, evname
-- This will build the even report dynamic, then send SECS-II message to host
--
--=============================================================================
function notifyEventID(ev_id, evname)
	print("BEGIN notifyEventID Begin"..ev_id.." EventName="..evname);
	local sqlStatement = "select * from eqpevent where sts=1 and ee_uuid='"..ev_id.."'";
	cur = con:execute(sqlStatement)
    -- Can not find ECID in DB
    local isFound = 1
	if cur==nil then
		isFound = 0
	end
    -- Can not find ECID in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		isFound = 0
	end
    -- 
    -- Can not found, do insert 
    if( isFound== 0) then
        print("notifyEventID cannot found EEID="..ev_id.." in the db config. Stop process");
    else
        -- Founnd SVID in table. Load value here
        local evid = {};
        -- Set data value
        DATA_ID = DATA_ID+1;
        evid["dataid"] = DATA_ID
        evid["ee_id"] = row.ee_id
        evid["ee_uuid"] = row.ee_uuid
        evid["ee_eventname"] = row.ee_eventname
        evid["report"] = {}
        cur:close()
        --
        -- get all link RPT with this event ID
        local sqlStatementReport = "select er.er_id, er.er_uuid, er_reportname, ee_id "..
            " from eqpreport er, linkevent2report ler"..
            " where er.er_id=ler.er_id"..
            " and er.sts=1"..
            --" and er.ee_uuid='"..ev_id.."'"..
            " and ler.ee_id="..evid["ee_id"];
        print ( sqlStatementReport );

        curReport = con:execute(sqlStatementReport);
        if curReport~=nil then
            -- Can not find ecid in DB
            rowLinkReport = curReport:fetch ({}, "a")
            --print ( rowLinkReport );
            --print_r ( rowLinkReport );
            -- Found ecid in table.
            --evid["report"] = {}
            while rowLinkReport do
                -- Set data value
                print_r ( rowLinkReport );
                evid["report"][rowLinkReport.er_uuid] = {}
                evid["report"][rowLinkReport.er_uuid]["er_id"] = rowLinkReport.er_id
                evid["report"][rowLinkReport.er_uuid]["er_uuid"] = rowLinkReport.er_uuid
                evid["report"][rowLinkReport.er_uuid]["er_reportname"] = rowLinkReport.er_reportname
                evid["report"][rowLinkReport.er_uuid]["ee_id"] = rowLinkReport.ee_id
                -- Do load linked variable for this report
                evid["report"][rowLinkReport.er_uuid]["linked"] = getLinkedVariableForReportID(rowLinkReport.er_id);
                -- Next record
                rowLinkReport = curReport:fetch ({}, "a")
                --print( "NEXT=========================" );
                --print ( rowLinkReport );
            end
            -- END OF LOAD REPORT ID FROM DB
            -- GET VARIABLE
            
            -- END OF GET VARIABLE
        end
        --
        print_r( evid );
        return evid
    end
	print("END notifyEventID");
	--return not nil val
	return nil
end

--=============================================================================
-- Load getLinkedVariableForReportID
-- input er_id
-- This will load list report id link to variable
--
--=============================================================================
function getLinkedVariableForReportID(er_id)
	print("BEGIN getLinkedVariableForReportID Begin er_id="..er_id);
    
	local sqlStatement = "select * "..
                         " from eqpdatavariable edv, linkreport2variable lrv"..
                         " where edv.dv_id=lrv.dv_id"..
                         " and edv.sts=1"..
                         " and lrv.er_id="..er_id;

	local cur = con:execute(sqlStatement)
    -- Can not find ECID in DB
    local isFound = 1
	if cur==nil then
		isFound = 0
	end
    -- Can not find ECID in DB
	local row = cur:fetch ({}, "a")
	if row==nil then
		isFound = 0
    else
        local svid_value_link = {};
        --print ( row );
        --print_r ( row );
        -- Found ecid in table.
        --evid["report"] = {}
        while row do
            -- Set data value
            --print_r( row );
            --local svid_value = {};
            
            local dv_datatype = tonumber(row.dv_datatype);
            svid_value_link[row.dv_id] = {};
            svid_value_link[row.dv_id]["dv_id"] = row.dv_id
            svid_value_link[row.dv_id]["dv_datatype"] = tonumber(row.dv_datatype)
            svid_value_link[row.dv_id]["dv_name"] = row.dv_name
            svid_value_link[row.dv_id]["dv_uuid"] = row.dv_uuid
            svid_value_link[row.dv_id]["dv_unit"] = row.dv_unit
            svid_value_link[row.dv_id]["dv_secs"] = row.dv_secs
            if(dv_datatype==4 or dv_datatype==5 or dv_datatype==6) then
                -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
                svid_value_link[row.dv_id]["dv_value"] = row.dv_valuetext
            else
                -- if this is the numeric value, get from field dv_value
                svid_value_link[row.dv_id]["dv_value"] = row.dv_value
            end
            -- Next record
            row = cur:fetch ({}, "a")
        end
        -- Founnd SVID in table. Load value here
        -- Set data value
        --
        
        --
        -- end update
        -- print_r( svid_value );
        return svid_value_link
    end
	--print("END notifyEventID");
	--return not nil val
	return nil
end
--=============================================================================
-- Load buildS6f11
-- input dta
-- This build the S6f11 SML
--
--=============================================================================
function buildS6f11(dta)
	print("BEGIN buildS6f11 Begin");
    print_r ( dta );
    local sml = [[S6F11=S6F11 W OUTPUT
<L
]]
    sml = sml.."\t<U4 "..dta["dataid"]..">\n";
    sml = sml.."\t<U4 "..dta["ee_uuid"]..">\n";
    sml = sml.."\t<L\n";
    for k, v in pairs(dta["report"]) do
        sml = sml.."\t\t<L\n";
            sml = sml.."\t\t\t<U4 "..v["er_uuid"]..">\n";--..dta["report"][i][""]..">";
            sml = sml.."\t\t\t<L\n";
                for k2, v2 in pairs(v["linked"]) do
                    if(tonumber(v2["dv_datatype"])==4 or tonumber(v2["dv_datatype"])==5 or tonumber(v2["dv_datatype"])==6) then
                        sml = sml.."\t\t\t\t<"..v2["dv_secs"].." '"..v2["dv_value"].."'>\n";
                    else
                        sml = sml.."\t\t\t\t<"..v2["dv_secs"].." "..v2["dv_value"]..">\n";
                    end
                end
            sml = sml.."\t\t\t>\n";
        sml = sml.."\t\t>\n";
    end
    sml = sml.."\t>\n";
sml = sml..">.\n";
    print( sml );
end