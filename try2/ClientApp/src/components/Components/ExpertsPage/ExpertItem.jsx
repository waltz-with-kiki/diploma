import React from "react";
import "./Experts.css"
const ExpertItem = React.memo(({ item, onSelect, isSelected }) => {
    return (
      <tr
        onClick={() => onSelect(item)}
        className={isSelected ? "selected" : ""}
      >
        <td className="column-surname">{item.surname}</td>
        <td className="column-name">{item.name}</td>
        <td className="column-patronymic">{item.patronymic}</td>
      </tr>
    );
  });
  
export default ExpertItem;