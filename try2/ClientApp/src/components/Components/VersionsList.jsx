import { useState, useEffect } from "react";
import MyButton from "./UI/MyButton";
import VersionItem from "./VersionItem";

const VersionsList = ({ remove, children , selectedProject, selectedVersion, ...props}) =>{

    const [selectedItem, setSelectedItem] = useState(null);

    const RemoveSelectedItem = (e) =>
    {
      e.preventDefault();
      console.log(selectedItem);
      remove(selectedItem);
    }

    useEffect(() => {
      
      setSelectedItem(null);
      selectedVersion(null);
  }, [selectedProject]);

    const handleSelectItem = (item) => {
        setSelectedItem(item);
        selectedVersion(item);
      }

    return(
        <div>
          <strong>{children}</strong>
          <MyButton onClick={RemoveSelectedItem}>Удалить</MyButton>
            {selectedProject.versions.map((item) => (
        <VersionItem
          key={item.id} 
          item={item}
          onSelect={handleSelectItem}
          isSelected={item === selectedItem}
        />
      ))}
    </div>
    );
}

export default VersionsList;