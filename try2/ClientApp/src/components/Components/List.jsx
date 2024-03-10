import { useState } from "react";
import ListItem from "./ListItem";
import MyButton from "./UI/MyButton";

const List = ({ remove, children , onSelectProject, ...props}) =>{

    const [selectedItem, setSelectedItem] = useState(null);

    const RemoveSelectedItem = (e) =>
    {
      e.preventDefault();
      console.log(selectedItem);
      remove(selectedItem);
    }

    const handleSelectItem = (item) => {
        setSelectedItem(item);
        onSelectProject(item);
      }

    return(
        <div>
          <strong>{children}</strong>
          <MyButton onClick={RemoveSelectedItem}>Meow</MyButton>
            {props.Projects.map((item) => (
        <ListItem
          key={item.id} // Убедитесь, что у ваших элементов есть уникальные ключи
          item={item}
          onSelect={handleSelectItem}
          isSelected={item === selectedItem}
        />
      ))}
    </div>
    );
}

export default List;