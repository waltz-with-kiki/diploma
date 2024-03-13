import React, { useState } from "react";
import ListItem from "./ListItem";
import MyButton from "./UI/MyButton";
import "../Projectstyles.css"

const List = ({ remove, children, ClearselectedVersion, onSelectProject, ...props }) => {
  const [selectedItem, setSelectedItem] = useState(null);

  const RemoveSelectedItem = (e, thisItemRemove) => {
    e.preventDefault();
    
    remove(thisItemRemove);
  };

  const handleSelectItem = (item) => {
    setSelectedItem(item);
    onSelectProject(item);
    ClearselectedVersion(null);
  };

  return (
    <div>
      <strong>{children}</strong>
    <div className="list">

      {props.Projects.map((item) => (
        <ListItem
          key={item.id} // Убедитесь, что у ваших элементов есть уникальные ключи
          item={item}
          onSelect={handleSelectItem}
          isSelected={item === selectedItem}
          onRemove={RemoveSelectedItem}
          onEdit={(editedItem) => {
            // Обработка изменений
            console.log("Edited Item:", editedItem);
          }}
        />
      ))}
    </div>
    </div>
  );
};

export default List;