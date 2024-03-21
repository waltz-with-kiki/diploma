import { useState } from "react";

const VersionItem = ({ item, onSelect, isSelected }) => {

    /*const RemoveItem = () => {
      console.log(item);
      remove(item);
    }*/
    const [isHovered, setIsHovered] = useState(false);

  const handleMouseEnter = () => {
    setIsHovered(true);
  };

  const handleMouseLeave = () => {
    setIsHovered(false);
  };

    return (
      <div
      style={{
        backgroundColor: isSelected ? 'rgba(173, 216, 230, 0.3)' : 'rgba(255, 255, 255, 1)', 
        padding: '10px',
        border: '1px solid #ccc',
        boxShadow: isSelected ? '0 0 10px 2px rgba(0, 128, 128, 0.7)' : 'none', 
        position: 'relative', 
      }}
        onClick={() => onSelect(item)}
        onMouseEnter={handleMouseEnter}
      onMouseLeave={handleMouseLeave}
      >
        <span>{item.n}.{item.nn}.{item.nnn}</span>

        {isHovered && (
        <div
          style={{
            position: 'absolute',
            top: '100%', 
            left: 0,
            backgroundColor: 'rgba(0, 0, 0, 0.7)',
            color: '#fff',
            padding: '5px',
            borderRadius: '5px',
            zIndex: 1, 
          }}
        >
          {item.descr}
          </div>
      )}
        
      </div>
    );
  }

export default VersionItem;