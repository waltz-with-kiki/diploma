import React, { useState } from "react";
import MyModal from "./Components/UI/MyModal/MyModal";
import MyButton from "./Components/UI/MyButton";
import "./Components/UI/MyModal/MyModal.css"

const ExpertAssessmentPage = () =>{

    const [modalActive, setModalActive] = useState(false);

    return(
        <div className="App">
            <h1>Экспертное оценивание</h1>
            <MyButton onClick={() => setModalActive(true)}>Чек тайм</MyButton>
            <MyModal active={modalActive} setActive={setModalActive}>
            <span>Слава богу</span>
        </MyModal>
        </div>
    );

}

export default ExpertAssessmentPage;