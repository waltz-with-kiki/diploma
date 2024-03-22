import React, { useState, useEffect, useMemo } from "react"
import MyButton from "./Components/UI/MyButton";
import MyInput from "./Components/UI/MyInput";
import ExpertsList from "./Components/ExpertsPage/ExpertsList";
import ExpertDetails from "./Components/ExpertsPage/ExpertDetails";
import "./Projectstyles.css";
import MyModal from "./Components/UI/MyModal/MyModal";
import AddForm from "./Components/ExpertsPage/AddForm";

const Experts = () => {

    const [Experts, setExperts] = useState([]);
    const [AircraftTypes, setAircraftTypes] = useState([]);
    const [selectedExpert, setSelectedExpert] = useState(null);
    const [searchExpert, setSearchExpert] = useState("");
    const [addModule, setAddModule] = useState(false);

    useEffect (() =>{

        fetchAircraftTypes();
        fetchExperts();
    }, []);

    const fetchExperts = async () =>{
        try{
        const response = await fetch('https://localhost:7150/api/accounts/experts')
        const data = await response.json();
        setExperts(data);
        console.log(data);
        }
        catch(error){
            console.error("Error fetching experts:", error);
        }
    }

    const fetchAircraftTypes = async () => {
        try{
        const response = await fetch('https://localhost:7150/api/accounts/aircrafttypes');
        const data = await response.json();
        setAircraftTypes(data);
        console.log(data);
        }
        catch(error){
            console.error("Error fetching experts:", error)
        }
    }

    const SearchedExperts = useMemo(() =>{
        if(searchExpert){
        return [...Experts].filter(expert => expert.name.toLowerCase().includes(searchExpert.toLowerCase()) || expert.surname.toLowerCase().includes(searchExpert.toLowerCase()) || expert.patronymic.toLowerCase().includes(searchExpert.toLowerCase()));
        }
        return Experts;
    }, [searchExpert, Experts])


    const ShowAddModule = () => {
        setAddModule(true);
    }

    const CheckNewExpert = (newexpert) => {
        console.log(newexpert);
    }


    return(
    <div className="page2">
    <span>Поиск эксперта БД:</span>
          <div className="searchexpert">
            <MyInput placeholder="Поиск.." value={searchExpert} onChange={e => setSearchExpert(e.target.value)} ></MyInput>
            </div>
            <div className="expertcomm">
              <MyButton onClick={ShowAddModule} className="button1">Добавить</MyButton>
              <MyButton className="button1">Изменить</MyButton>
              <MyButton className="button1">Удалить</MyButton>
            </div>
            
                <div className="ExpertPanel">
                    <div className="ExpertPanel left">
                <ExpertsList  onSelect={setSelectedExpert} Experts={SearchedExperts}></ExpertsList>
                </div>
                <div className="ExpertPanel right">
                <ExpertDetails Expert={selectedExpert}></ExpertDetails>
                </div>
                </div>

                <div>
                    <AddForm aircrafttypes={AircraftTypes} addModule={addModule} setAddModule={setAddModule} AddnewExpert={CheckNewExpert}></AddForm>
                </div>
    </div>
    );
}

export default Experts;
