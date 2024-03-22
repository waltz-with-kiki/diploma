import React, { useState } from "react";

const AircraftTypeItem = ({aircraft, UpdateAircraftTypes, isSelected}) =>{

    //const [isSelect, setIsSelect] = useState(false);


    const handleUpdateAircraftTypes = () => {
        UpdateAircraftTypes(aircraft.name, !isSelected);
    }

    /*const handleUpdateAircraftTypes = () => {
        const newValue = !isSelect;
        setIsSelect(newValue);
        UpdateAircraftTypes(aircraft.name, newValue);
    }*/

    return(
        <tr>
            <td><input type="checkbox" checked={isSelected} onChange={handleUpdateAircraftTypes}></input></td>
            <td>{aircraft.name}</td>
        </tr>
    );
}

export default AircraftTypeItem;