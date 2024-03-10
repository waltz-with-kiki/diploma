import React, { useState, useEffect } from "react";
import MyButton from "./Components/UI/MyButton";
import MyInput from "./Components/UI/MyInput";
import List from "./Components/List";
import VersionsList from "./Components/VersionsList";

const Project = () => {

    const [selectedProject, setSelectedProject] = useState(null);
    const [Projects, setProjects] = useState([]);
    const [Versions, setVersions] = useState([]);
    const [NewProject, setNewProject] = useState({ name: ''});

    const AddNewProject = (e) =>{
        e.preventDefault();
        const Project = {
            ...NewProject,
        }
        setProjects((prevProjects) => [...prevProjects, Project]);
        setNewProject({ name: '' });
    }

    const DeleteProject = async (project) => {
      
      /*const response = await fetch('https://localhost:7150/api/accounts/profiles/remove', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({nickName: profilenickName
                }),
            });
      */

      const UpdateProjects = Projects.filter((element) => element.name !== project.name);

      setProjects(UpdateProjects);
      
    }


    useEffect(() => {
      // Вызов метода GET при монтировании компонента
      fetchProjects();
    }, []);
  
    const fetchProjects = async () => {
      try {
        const response = await fetch('https://localhost:7150/api/accounts/projects');
        const data = await response.json();
        setProjects(data);
        console.log(data);
      } catch (error) {
        console.error("Error fetching projects:", error);
      }
    };

    
    return(
        <div>
            <MyInput value={NewProject.name} onChange={(e) => setNewProject({...NewProject, name: e.target.value})}></MyInput>
            <MyButton onClick={AddNewProject}>Добавить</MyButton>
            <List remove={DeleteProject} Projects={Projects} onSelectProject={setSelectedProject}>Проекты</List>
            
            {selectedProject && (
        <VersionsList remove={DeleteProject} selectedProject={selectedProject}>
          Версии
        </VersionsList>
      )}
            

        </div>
    );

}

export default Project;