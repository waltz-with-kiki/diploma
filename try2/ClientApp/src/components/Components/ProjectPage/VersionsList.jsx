import { useState, useEffect } from "react";
import MyButton from "../UI/MyButton";
import VersionItem from "./VersionItem";
import "../../Projectstyles.css"

const VersionsList = ({ remove, children , versions, selectedVersion, ...props}) =>{

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
  }, [versions]);

    const handleSelectItem = (item) => {
        setSelectedItem(item);
        selectedVersion(item);
      }

    return(
      <div>
        <div>
  <strong>{children}</strong>
  <div className="list">
    {versions === null ? (
      <h1>Версий нет</h1>
    ) : (
      versions.map((item) => (
        <VersionItem
          key={item.id} 
          item={item}
          onSelect={handleSelectItem}
          isSelected={item === selectedItem}
        />
      ))
    )}
  </div>
</div>
    </div>
    );
}

export default VersionsList;