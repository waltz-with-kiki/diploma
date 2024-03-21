import React, { Children } from "react";

const MySpan = ({point, children}) => {

    return(
        <div>
            <span>
                {point}: {children}
            </span>
        </div>
    );
}

export default MySpan;