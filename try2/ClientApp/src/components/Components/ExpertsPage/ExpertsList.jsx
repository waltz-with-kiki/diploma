import React, { useState } from "react";
import ExpertItem from "./ExpertItem";
import "./Experts.css"

const ExpertsList = ({Experts, onSelect, ...props}) => {

    const [selectedExpert, setSelectedExpert] = useState(null);

    const handleExpertClick = (expert) => {
        setSelectedExpert(expert);
        onSelect(expert);
        console.log(expert)
    }

    return(
        <div>
            <table className="table-container">
                <thead>
                  <tr>
                    <th className="column-surname">Фамилия</th>
                    <th className="column-name">Имя</th>
                    <th className="column-patronymic">Отчество</th>
                  </tr>
                </thead>
                <tbody>
                    {Experts.map((expert) =>(
                        <ExpertItem
                        item = {expert}
                        onSelect={() => handleExpertClick(expert)}
                        isSelected={expert === selectedExpert}
                        ></ExpertItem>
                    ))}
                </tbody>
              </table>
        </div>
    );
}


export default ExpertsList;