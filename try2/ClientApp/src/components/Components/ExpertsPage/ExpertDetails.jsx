import React, {useState, useEffect} from "react";
import MySpan from "../UI/MySpan/MySpan";

const ExpertDetails = ({Expert}) => {

    const [expert, setExpert] = useState(null)

    useEffect(() => {
        setExpert(Expert);
      }, [Expert]);

    return(
        <div>
      {expert ? (
        <>
          <MySpan point={"Фамилия"}>{expert.surname}</MySpan>
          <MySpan point={"Имя"}>{expert.name}</MySpan>
          <MySpan point={"Отчество"}>{expert.patronymic}</MySpan>
          <MySpan point={"Год рождения"}>{expert.birthYear}</MySpan>
          <MySpan point={"Летный стаж, г"}>{expert.serviceYear}</MySpan>
          <MySpan point={"Налёт, ч"}>{expert.flightHours}</MySpan>
          <MySpan point={"Лётный класс"}>{expert.pilotClass}</MySpan>
          <MySpan point={"Тип пилотируемых ЛА"}>{expert.aircraftTypeForExperts.map((aircraftType) => aircraftType.aircraftType.name).join(', ')}</MySpan>
          <MySpan point={"Образование"}>{expert.educationNavigation && expert.educationNavigation.name}</MySpan>
        </>
      ) : (
        <p>Выберите эксперта</p>
      )}
    </div>
    );
}

export default ExpertDetails

           