import React, { useState, useEffect } from "react";
import MyButton from "../UI/MyButton";
import MyInput from "../UI/MyInput";
import "../../Projectstyles.css"

const ListItem = ({ item, onSelect, isSelected, onEdit, onRemove, ...props}) => {
  const [isEditing, setEditing] = useState(false);
  const [editedName, setEditedName] = useState(item.name);

  const handleRemove = (e) => {
    e.stopPropagation();
    onRemove(e, item);
  }

  const handleBlur = () => {
    console.log(item);
    console.log(editedName);
    onEdit(item, editedName);
    setEditedName(item.name)
    setEditing(false);
  };

  const handleInputChange = (e) => {
    setEditedName(e.target.value.trim());
  };

  const handleEdit = () => {
    setEditing(true);
  };


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
        <MyInput
          type="text"
          value={editedName}
          onChange={handleInputChange}
          onBlur={handleBlur}
        />
      ) : (
        <span className="span">{item.name}</span>
      )}
      
          <div className="button-container">
          <MyButton className="button1" onClick={handleEdit}>Изменить</MyButton>
          <MyButton className="button1" onClick={handleRemove}>Удалить</MyButton>
          </div>
        </div>

  );
};

export default ListItem;