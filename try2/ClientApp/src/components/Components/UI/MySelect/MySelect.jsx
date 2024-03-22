import React from "react";

const MySelect = ({baseOption, options, ...props}) => {

    return(
        <div>
            <select {...props}>
                <option disabled>{baseOption}</option>
                {options.map((option) =>
                    <option key={option.value} value={option.value}>
                        
                        {option.name}
                    </option>
                )}
            </select>
        </div>
    );
}

export default MySelect;