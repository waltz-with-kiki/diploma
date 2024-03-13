import React, { useState, useEffect } from "react";
import MyButton from "./Components/UI/MyButton";
import MyInput from "./Components/UI/MyInput";
import List from "./Components/List";
import VersionsList from "./Components/VersionsList";
import FormVersion from "./Components/FormVersion";
import "./Projectstyles.css";
import MyModal from "./Components/UI/MyModal/MyModal";

const Project = () => {

  const [selectedProject, setSelectedProject] = useState(null);
  const [selectedVersion, setSelectedVersion] = useState(null);
  const [Projects, setProjects] = useState([]);
  const [NewProject, setNewProject] = useState({ name: '' });
  const [isFormVisible, setFormVisible] = useState(false);
  const [changeAddForm, setChangeAddForm] = useState(false);



  const showForm = (changeAddForm) => {
    setFormVisible(true);
    setChangeAddForm(changeAddForm);
  };

  const hideForm = () => {
    setFormVisible(false);
  };

  const handleAddVersion = async (newVersion) => {


    console.log(selectedProject);
    console.log(newVersion);

    console.log(selectedProject);

    const response = await fetch('https://localhost:7150/api/accounts/addversion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        ProjectId: selectedProject.id, N: newVersion.n, Nn: newVersion.nn, Nnn: newVersion.nnn, Descr: newVersion.descr
      }),
    });

    hideForm();

    fetchProjects();


  };

  const handleChangeVersion = (selectedVersion) => {

  }

  const AddNewProject = async (e) => {
    e.preventDefault();
    const Project = {
      ...NewProject,
    }

    console.log(Project);

    const response = await fetch('https://localhost:7150/api/accounts/addproject', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Name: Project.name
      }),
    });

    //setProjects((prevProjects) => [...prevProjects, Project]);
    fetchProjects();
    setNewProject({ name: '' });
  }

  const DeleteProject = async (project) => {

    console.log(project);
    console.log(selectedProject);

    if (project === selectedProject){
    setTimeout(() => {
      setSelectedProject(null);
    }, 0);
  }

    const response = await fetch('https://localhost:7150/api/accounts/removeproject', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Name: project.name
      }),
    });

    fetchProjects();

  }

  const EditProject = (project) => {
    // Ваша логика редактирования
    console.log("Edit clicked in App:", project);
  };

  const DeleteVersion = async (version) => {

    console.log(selectedProject);
    console.log(version);

    

    const response = await fetch('https://localhost:7150/api/accounts/removeversion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        ProjectId: selectedProject.id, N: version.n, Nn: version.nn, Nnn: version.nnn, Descr: version.descr
      }),
    });

    fetchProjects();
    /*  const projectToUpdate = Projects.find((p) => p.name === project.name);

      if (projectToUpdate) {
        const updatedVersions = projectToUpdate.versions.filter((v) => v.id !== version.id);

        projectToUpdate.versions = updatedVersions;

        setProjects((prevProjects) =>
  prevProjects.map((p) =>
    p.name === projectToUpdate.name ? projectToUpdate : p
  )
);
      }*/

    //setProjects(UpdateProjects);
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


  return (
    <div className="page">
      <MyInput style={{marginBottom: "10px"}} value={NewProject.name} onChange={(e) => setNewProject({ ...NewProject, name: e.target.value })}></MyInput>
      <MyButton onClick={AddNewProject}>Добавить</MyButton>
      <div className="left-section">
      <List remove={DeleteProject} Projects={Projects} ClearselectedVersion={setSelectedVersion} onSelectProject={setSelectedProject} onEditProject={EditProject}>Проекты</List>
      </div>

      <div>
          <MyButton onClick={selectedProject ? () => showForm(true) : () => {}}>Добавить</MyButton>
          <MyButton onClick={selectedVersion ? () => showForm(false) : () => {}}>Изменить</MyButton>
      </div>

      <div className="right-section">
      {(selectedProject && selectedProject.versions && selectedProject.versions.length > 0) && (
        <VersionsList remove={DeleteVersion} selectedProject={selectedProject} selectedVersion={setSelectedVersion} >
          Версии
        </VersionsList>
      )}
      </div>
      
      {isFormVisible && (
        <div>
          {changeAddForm
            ? <FormVersion active={isFormVisible} SetActive={setFormVisible} onAddVersion={handleAddVersion} onCancel={hideForm}></FormVersion>
            : <FormVersion active={isFormVisible} SetActive={setFormVisible} onAddVersion={handleChangeVersion} selectedVersion={selectedVersion} onCancel={hideForm}></FormVersion>}
        </div>
      )}
      
    </div>
  );

}

export default Project;