import React, { Children } from "react";
import "./MyModal.css";
import MyButton from "../MyButton";

const MyModal = ({active, setActive, children}) =>{

    return(
        <div className={active ? "modal active" : "modal"} onClick={() => setActive(false)}>
            <div className="modal_content">
                {children}
            </div>
        </div>
    );
}



export default MyModal;