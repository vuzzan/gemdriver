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

--=============================================================================
-- Load equipment SVID from DB
-- input svid
--=============================================================================
function loadSVIDfromDB(svid)
	--print("BEGIN loadSVIDfromDB Begin"..svid);
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_type=2 and dv_uuid='"..svid.."'";
	cur = con:execute(sqlStatement)
    -- Can not find SVID in DB
	if cur==nil then
		return nil
	end
    -- Can not find SVID in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		return nil
	end
	-- Founnd SVID in table.
	local svid_value = {};
	-- Set data value
    svid_value["dv_id"] = row.dv_id;
    svid_value["type"] = row.dv_datatype
	svid_value["name"] = row.dv_name
	svid_value["uuid"] = row.dv_uuid
	svid_value["dv_unit"] = row.dv_unit
    if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
        -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
		svid_value["value"] = row.dv_valuetext
	else
        -- if this is the numeric value, get from field dv_value
		svid_value["value"] = row.dv_value
	end
	--print("END loadSVIDfromDB Begin"..svid);
	--return not nil val
	return svid_value
end

--=============================================================================
-- Load loadECIDfromDB
-- input ID
--=============================================================================
function loadECIDfromDB(ecid)
	--print("BEGIN loadECIDfromDB Begin"..ecid);
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_type=1 and dv_uuid='"..ecid.."' limit 1";
	cur = con:execute(sqlStatement)
    -- Can not find ecid in DB
	if cur==nil then
		return nil
	end
    -- Can not find ecid in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		return nil
	end
	-- Founnd ecid in table.
	local ecid_value = {};
	-- Set data value
    ecid_value["type"] = row.dv_datatype
	ecid_value["name"] = row.dv_name
	ecid_value["uuid"] = row.dv_uuid
	ecid_value["dv_unit"] = row.dv_unit
	ecid_value["dv_default"] = row.dv_default
	ecid_value["dv_limitmin"] = row.dv_limitmin
	ecid_value["dv_limitmax"] = row.dv_limitmax
    if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
        -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
		ecid_value["value"] = row.dv_valuetext
	else
        -- if this is the numeric value, get from field dv_value
		ecid_value["value"] = row.dv_value
	end
	--print("END loadECIDfromDB Begin"..ecid);
	--return not nil val
	return ecid_value
end

--=============================================================================
-- Load loadECIDfromDBAll
-- input 
--=============================================================================
function loadECIDfromDBAll()
	print("BEGIN loadECIDfromDB Begin all");
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_type=1";
	cur = con:execute(sqlStatement)
    -- Can not find ecid in DB
	if cur==nil then
        print("NIL 1");    
		return nil
	end
	local ecid_value = {};
    -- Can not find ecid in DB
	row = cur:fetch ({}, "a")
	-- Found ecid in table.
    while row do
        -- Set data value
        --print( "NAME="..row.dv_uuid );
        ecid_value[row.dv_uuid] = {}
        ecid_value[row.dv_uuid]["type"] = row.dv_datatype
        ecid_value[row.dv_uuid]["name"] = row.dv_name
        ecid_value[row.dv_uuid]["uuid"] = row.dv_uuid
        ecid_value[row.dv_uuid]["dv_unit"] = row.dv_unit
        ecid_value[row.dv_uuid]["dv_default"] = row.dv_default
        ecid_value[row.dv_uuid]["dv_limitmin"] = row.dv_limitmin
        ecid_value[row.dv_uuid]["dv_limitmax"] = row.dv_limitmax
        if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
            -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
            ecid_value[row.dv_uuid]["value"] = row.dv_valuetext
        else
            -- if this is the numeric value, get from field dv_value
            ecid_value[row.dv_uuid]["value"] = row.dv_value
        end
        row = cur:fetch ({}, "a")
    end
	--print("END loadECIDfromDB Begin"..ecid);
	--return not nil val
	return ecid_value
end

--=============================================================================
-- Load saveECIDtoDB
-- input id, value, datatype
--=============================================================================
function saveECIDtoDB(ecid, ecid_value,secs_data_type)
	--print("BEGIN loadSVIDfromDB Begin"..svid);
	local sqlStatement = "select * from eqpdatavariable where sts=1 and dv_type=1 and dv_uuid='"..ecid.."'";
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
        svid_value["dv_id"] = row.dv_id
        svid_value["type"] = row.dv_datatype
        svid_value["name"] = row.dv_name
        svid_value["uuid"] = row.dv_uuid
        svid_value["dv_unit"] = row.dv_unit
        -- sqlStatement prepare
        sqlStatement = "update eqpdatavariable set ";
        if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
            -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
            svid_value["value"] = row.dv_valuetext
            sqlStatement = sqlStatement.. " dv_valuetext='"..ecid_value.."' ";
        else
            -- if this is the numeric value, get from field dv_value
            svid_value["value"] = row.dv_value
            sqlStatement = sqlStatement.." dv_value='"..ecid_value.."' ";
        end
        sqlStatement = sqlStatement.." where dv_id="..svid_value["dv_id"].."";
        -- Call to db to udpate query
        print(sqlStatement)
        con:execute(sqlStatement)
        --
        -- 
        -- end update
    end
	--print("END loadSVIDfromDB Begin"..svid);
	--return not nil val
	return svid_value
end

--=============================================================================
-- Load createnewlinkReport2Variable
-- input id, vid
--=============================================================================
function createnewlinkReport2Variable(rpt_id, vid)
    if rpt_id <0 then 
        return -1;
    end
    --
    dv = loadSVIDfromDB(vid);
    --print_r( dv );
    if (dv~=nil) then
        dv_id = dv.dv_id;
        local sql="insert into linkreport2variable(er_id, dv_id,eqp_id) values("..rpt_id..","..dv_id..","..EQP_ID..")";
        con:execute(sql);
        return 1;
    else
        print("Can not found VID="..vid.." in system...");
        return -1;
    end
    --
end
function deleteReportIDfromDB(rpt_uuid, only_link)
    local sql = "select * from eqpreport where sts=1 and er_uuid='"..rpt_uuid.."'";
    cur = assert (con:execute(sql))
    --
    --print ( sql );
    local er_id = -1;
    rpt = cur:fetch ({}, "a")
    while rpt do
        -- do delete row and all rptid link to
        er_id = tonumber( rpt.er_id );
        -- Delete link report to variable
        con:execute("delete from linkreport2variable where er_id="..er_id);
        -- end
        rpt = cur:fetch ({}, "a")
    end
    ---
    if(er_id>0 and only_link==false) then
        con:execute("delete from eqpreport where er_id="..er_id);
    end
    -- Create new report ID
    if (only_link==true and er_id == -1 ) then
        er_id = get_val("select max(er_id)+1 from eqpreport");
        con:execute("insert into eqpreport values ("..er_id..","..EQP_ID..",'"..rpt_uuid.."','s2f33_"..rpt_uuid.."', 1)");
    end
    --
    return er_id
    ---
end

function deleteReportlinkToCEIDfromDB(ceid_uuid)
    cur = assert (con:execute("select * from eqpevent where sts=1 and ee_uuid='"..ceid_uuid.."'"))
    --
    local ee_id = -1;
    row = cur:fetch ({}, "a")
    if( row==nil ) then
        -- Can not found ceid_uuid
    else
        -- do delete row and all rptid link to
        ee_id = tonumber( row.ee_id );
        ---
        if(ee_id>0) then
            con:execute("delete from linkevent2report where ee_id="..ee_id);
        end
    end
    --
    return ee_id
    ---
end

function createnewlinkCEID2Report(ceid, er_uuid)
    if ceid <0 then 
        return -1;
    end
    -- Load report from db by er_uuid
    cur = assert (con:execute("select * from eqpreport where sts=1 and er_uuid='"..er_uuid.."'"))
    
    row = cur:fetch ({}, "a")
    --
    if(row~=nil) then
        er_id = row.er_id;
        local sql="insert into linkevent2report(ee_id, er_id, eqp_id) values("..ceid..","..er_id..","..EQP_ID..")";
        con:execute(sql);
        return 1;
    else
        print("Can not found RPTID="..er_uuid.." in system...");
        return -1;
    end
    --
end
function setEnableDisableCEIDAll(ceed )
    -- Enable/Diable all
    con:execute("update eqpevent set sts="..ceid);
end
function setEnableDisableCEID(ceid_uuid, ceed )
    --
    ceid_value = loadCEIDfromDB(ceid_uuid);
    if ceid_value==nil then
        -- Can not found CEID in db
        return -1;
    else
        -- update value
        con:execute("update eqpevent set sts="..ceid.." where ee_id="..ceid_value.ee_id);
    end
end

function loadCEIDfromDB(ceid_uuid)
	print("BEGIN loadECIDfromDB Begin.. ceid_uuid="..ceid_uuid);
	local sqlStatement = "select * from eqpevent where ee_uuid='"..ceid_uuid.."' limit 1";
	print("BEGIN loadECIDfromDB Begin: "..sqlStatement);
	cur = con:execute(sqlStatement)
    -- Can not find ecid in DB
	if cur==nil then
		return nil
	end
    -- Can not find ecid in DB
	row = cur:fetch ({}, "a")
	if row==nil then
		return nil
	end
	-- Founnd ecid in table.
	local ceid_value = {};
	-- Set data value
    ceid_value["er_id"] = row.er_id;
	ceid_value["er_uuid"] = row.er_uuid;
	return ceid_value;
end
function logger(txt)
    local sqlStatement= "insert into actionlog(log_text,ip,log_time,user_id) values ('"..txt.."','',"..os.time(os.date("!*t"))..",0)";
    --print( sqlStatement );
    con:execute(sqlStatement)
end
function get_val(sqlStatement)
	cur = con:execute(sqlStatement)
    -- Can not find 
	if cur==nil then
		return nil
	end
    -- Can not find 
	row = cur:fetch ({}, "n")
	if row==nil then
		return nil
	end
    --
    --print_r (row );
    return tonumber(row[1]);
    --
end

function get_row(sqlStatement)
	cur = con:execute(sqlStatement)
    -- Can not find 
	if cur==nil then
		return nil
	end
    -- Can not find 
	row = cur:fetch ({}, "a")
	if row==nil then
		return nil
	end
    --
    return row
    --
end

function get_results(sqlStatement)
    ret = {}
	cur = con:execute(sqlStatement)
    -- Can not find 
	if cur==nil then
		return nil
	end
    repeat
        row = cur:fetch ({}, "a")
        table.insert(ret, row)
        -- end
    until ( row )
    --
    return ret
    --
end
