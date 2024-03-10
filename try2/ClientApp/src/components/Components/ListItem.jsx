import React from "react";

const ListItem = ({ item, onSelect, isSelected }) => {

    /*const RemoveItem = () => {
      console.log(item);
      remove(item);
    }*/

    return (
      <div
      style={{
        backgroundColor: isSelected ? 'rgba(173, 216, 230, 0.3)' : 'rgba(255, 255, 255, 1)', 
        padding: '10px',
        border: '1px solid #ccc',
        boxShadow: isSelected ? '0 0 10px 2px rgba(0, 128, 128, 0.7)' : 'none', 
      }}
        onClick={() => onSelect(item)}
      >
        <span>{item.name}</span>
      </div>
    );
  }

export default ListItem;