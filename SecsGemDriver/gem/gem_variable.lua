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
-- Load all SECSGEM static variable from database
SOFTREF = "TinyGem200.1.01"
EQP_NAME = "EQPNAME"
EQP_ID = 1

-- Initialize data list
cur = assert (con:execute("select * from equipment where sts=1"))
row = cur:fetch ({}, "a")
EQP_NAME = string.format("TinyGEM for %s", row.eqp_name)
EQP_ID = row.eqp_id

secsdatatype = {}
cur = assert (con:execute("select * from secsdatatype where sts=1"))
--repeat
    row = cur:fetch ({}, "a")
    secsdatatype[row.sdt_name] = {row.sdt_type, row.sdt_id}
--while( row )

--=============================================================================
-- Load equipment constant from DB
--=============================================================================
cur = con:execute("select * from eqpdatavariable where sts=1 and (dv_type=1 or dv_type=2)")
GEMvar = {};
-- Can not find ecid in DB
row = cur:fetch ({}, "a")
-- Found ecid in table.
while row do
    -- Set data value
    --print( "NAME="..row.dv_uuid );
    if( tonumber(row.dv_uuid) < 100 ) then
        GEMvar[row.dv_name] = {}
        GEMvar[row.dv_name]["type"] = row.dv_datatype
        GEMvar[row.dv_name]["name"] = row.dv_name
        GEMvar[row.dv_name]["uuid"] = row.dv_uuid
        GEMvar[row.dv_name]["dv_unit"] = row.dv_unit
        GEMvar[row.dv_name]["dv_default"] = row.dv_default
        GEMvar[row.dv_name]["dv_limitmin"] = row.dv_limitmin
        GEMvar[row.dv_name]["dv_limitmax"] = row.dv_limitmax
        if(row.dv_datatype==4 or row.dv_datatype==5 or row.dv_datatype==6) then
            -- if this is the text value, ASCII, JTS8....4, 5, 6. Will load data from dv_valuetext
            GEMvar[row.dv_name]["value"] = row.dv_valuetext
        else
            -- if this is the numeric value, get from field dv_value
            GEMvar[row.dv_name]["value"] = row.dv_value
        end
    end
    row = cur:fetch ({}, "a")
end
--print_r( GEMvar );
--=============================================================================
-- Load equipment constant from DB
--=============================================================================


