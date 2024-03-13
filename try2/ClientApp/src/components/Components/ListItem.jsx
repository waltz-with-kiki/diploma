import React, { useState, useEffect } from "react";
import MyButton from "./UI/MyButton";
import "../Projectstyles.css"

const ListItem = ({ item, onSelect, isSelected, onEdit, onRemove, ...props}) => {
  const [isEditing, setEditing] = useState(false);
  const [editedName, setEditedName] = useState(item.name);
  const [isMenuVisible, setMenuVisible] = useState(false);

  const handleRemove = (e) => {
    e.stopPropagation();
    onRemove(e, item);
  }

  return (
    <div
    className="project"
      id={`list-item-${item.id}`}
      style={{
        backgroundColor: isSelected
          ? "rgba(173, 216, 230, 0.3)"
          : "rgba(255, 255, 255, 1)",
        
        boxShadow: isSelected
          ? "0 0 10px 2px rgba(0, 128, 128, 0.7)"
          : "none",
      }}
      onClick={() => onSelect(item)}
    >
      {isEditing ? (
        // Если происходит редактирование, отображаем поле ввода
        <input
          type="text"
         // value={editedName}
         // onChange={handleInputChange}
        />
      ) : (
        // Если не происходит редактирование, отображаем статичный текст
        <span className="span">{item.name}</span>
      )}
      
          {/* Ваша панель */}
          <div className="button-container">
          <MyButton className="button1" >Изменить</MyButton>
          <MyButton className="button1" onClick={handleRemove}>Удалить</MyButton>
          </div>
        </div>

  );
};

export default ListItem;