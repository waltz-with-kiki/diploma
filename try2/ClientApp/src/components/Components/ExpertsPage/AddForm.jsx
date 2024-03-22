import React, { useState } from "react";
import MyModal from "../UI/MyModal/MyModal";
import MyInput from "../UI/MyInput";
import MyButton from "../UI/MyButton";
import MySelect from "../UI/MySelect/MySelect";
import AircraftTypeItem from "./AircraftTypeItem";

const AddForm = ({addModule, setAddModule, aircrafttypes, AddnewExpert}) => {


    const [newExpert , setNewExpert] = useState({name: "", surname: "", patronymic: "", birthYear: "", serviceYear: "", flightHours: "", pilotClass: '1', educationNavigation: 'secondary', expertAircraftTypes: []})


    const [selectedAircraftTypes, setSelectedAircraftTypes] = useState([]);

    const handleAddnewExpert = (e) =>{
        e.preventDefault();
        const UpdateExpert = {
            ...newExpert,
            expertAircraftTypes: selectedAircraftTypes
        }
        AddnewExpert(UpdateExpert);
        setSelectedAircraftTypes([]);
        setNewExpert({name: "", surname: "", patronymic: "", birthYear: "", serviceYear: "", flightHours: "", pilotClass: '1', educationNavigation: 'secondary', expertAircraftTypes: [] });
    }

    const UpdateAircraftTypesForExpert = (aircraftType, flag) => {
        if (flag === true){
            setSelectedAircraftTypes([...selectedAircraftTypes, aircraftType])
        }
        else {
            setSelectedAircraftTypes([selectedAircraftTypes.filter((x) => x.name !== aircraftType)]);
        }
        console.log(selectedAircraftTypes);
    }

    return(
        <div>
            <MyModal active={addModule} setActive={setAddModule} width="700px" height="700px">
            <MyInput placeholder={"Фамилия..."} value={newExpert.surname} onChange={(e) => setNewExpert({...newExpert, surname: e.target.value})}></MyInput>
            <MyInput placeholder={"Имя..."} value={newExpert.name} onChange={(e) => setNewExpert({...newExpert, name: e.target.value})}></MyInput>
            <MyInput placeholder={"Отчество..."} value={newExpert.patronymic} onChange={(e) => setNewExpert({...newExpert, patronymic: e.target.value})}></MyInput>
            <MyInput placeholder={"Год рождения..."} value={newExpert.birthYear} onChange={e => setNewExpert({...newExpert, birthYear: e.target.value})}></MyInput>
            <MyInput placeholder={"Летний стаж, г..."} value={newExpert.serviceYear} onChange={(e) => setNewExpert({...newExpert, serviceYear: e.target.value})}></MyInput>
            <MyInput placeholder={"Налёт, ч:"} value={newExpert.flightHours} onChange={e => setNewExpert({...newExpert, flightHours: e.target.value})}></MyInput>
            <MySelect value={newExpert.pilotClass}
            onChange={(e) => setNewExpert({...newExpert, pilotClass: e.target.value})}
            baseOption="Лётный класс"
            options={[
                {value: '1', name: 'Первый'},
                {value: '2', name: 'Второй'},
                {value: '3', name: 'Третий'}
                
            ]}

            />
            <MySelect value={newExpert.educationNavigation}
            onChange={(e) => setNewExpert({...newExpert, educationNavigation: e.target.value})}
            baseOption="Образование"
            options={[
                {value: 'secondary', name: 'Среднее'},
                {value: 'higher', name: 'Высшее'},
                {value: 'incomplete higher', name: 'Неполное высшее'}
            ]}
            />
            <table>
                <thead>
                    <tr>
                        <th>Выбор</th>
                        <th>Тип ЛА</th>
                    </tr>
                </thead>
                <tbody>
                    {aircrafttypes.map((aircraftType) =>(
                        <AircraftTypeItem
                        key={aircraftType.id}
                        aircraft={aircraftType}
                        isSelected={selectedAircraftTypes.includes(aircraftType.name)}
                        UpdateAircraftTypes={UpdateAircraftTypesForExpert}
                        />
                    ))}
                </tbody>
            </table>
            
            <MyButton>Добавить тип ЛА в БД</MyButton>
            <MyButton>Удалить тип ЛА из БД</MyButton>
            <MyButton onClick={handleAddnewExpert}>Добавить эксперта</MyButton>
            </MyModal>        
        </div>
    );
}

export default AddForm;