import { useState } from "react";
import MyButton from "./UI/MyButton";
import VersionItem from "./VersionItem";

const VersionsList = ({ remove, children , selectedProject, ...props}) =>{

    const [selectedItem, setSelectedItem] = useState(null);

    const RemoveSelectedItem = (e) =>
    {
      e.preventDefault();
      console.log(selectedItem);
      remove(selectedItem);
    }

    const handleSelectItem = (item) => {
        setSelectedItem(item);
      }

    return(
        <div>
          <strong>{children}</strong>
          <MyButton onClick={RemoveSelectedItem}>Meow</MyButton>
            {selectedProject.versions.map((item) => (
        <VersionItem
          key={item.id} // Убедитесь, что у ваших элементов есть уникальные ключи
          item={item}
          onSelect={handleSelectItem}
          isSelected={item === selectedItem}
        />
      ))}
    </div>
    );
}

export default VersionsList;