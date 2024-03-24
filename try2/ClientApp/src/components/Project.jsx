import React, { useState, useEffect, useMemo } from "react";
import MyButton from "./Components/UI/MyButton";
import MyInput from "./Components/UI/MyInput";
import List from "./Components/ProjectPage/List";
import VersionsList from "./Components/ProjectPage/VersionsList";
import FormVersion from "./Components/ProjectPage/FormVersion";
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
// Желательно будет исправить
    setSelectedProject(null);
    //Как-то обновлять список

    fetchProjects();


  };

  const handleChangeVersionPreCheck = (newVersion) =>{

    const Version = selectedProject.versions.find(version => 
      version.n === newVersion.n && 
      version.nn === newVersion.nn && 
      version.nnn === newVersion.nnn
  );

  if (Version == null){
    selectedVersion.n = newVersion.n;
      selectedVersion.nn = newVersion.nn;
      selectedVersion.nnn = newVersion.nnn;
      selectedVersion.descr = newVersion.descr;

      handleChangeVersion(selectedVersion);
  }

  }

  const handleChangeVersion = async (selectedVersion1) => {
      console.log("Проверка изменения");

      console.log(selectedVersion1);

      const response = await fetch('https://localhost:7150/api/accounts/changeversion', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Id: selectedVersion.id, ProjectId: selectedProject.id, N: selectedVersion.n, Nn: selectedVersion.nn, Nnn: selectedVersion.nnn, Descr: selectedVersion.descr
      }),
    });

    hideForm();
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

  const EditProjectPreCheck = (project, editedName) => {
    console.log(project);
    console.log(editedName);
    const newProjectName = Projects.find(iproject =>
      iproject.name === editedName
      );

    if (newProjectName == null && editedName.length >= 3){
        project.name = editedName;
        EditProject(project);
    }
  };

  const EditProject = async (project) =>{

    console.log(project);

    const response = await fetch('https://localhost:7150/api/accounts/editproject', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        Id: project.id, Name: project.name 
      }),
    });

  }

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


    //Желательно поменять логику
    setTimeout(() => {
      setSelectedProject(null);
    }, 0);

    fetchProjects();
    
  }

  const SortedVersions = useMemo(() => {
    if (selectedProject && selectedProject.versions) {
      return [...selectedProject.versions].sort((a, b) => {
        if (a.n !== b.n) {
          return b.n - a.n; 
        }
        if (a.nn !== b.nn) {
          return b.nn - a.nn; 
        }
        return b.nnn - a.nnn; 
      });
    }
    return [];
  }, [selectedProject]);

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
    <div>

    <div className="page">
      
      <div className="formversion">
        <div className="formversion left">
      <MyInput style={{marginRight: "10px", margin: "3px"}} value={NewProject.name} onChange={(e) => setNewProject({ ...NewProject, name: e.target.value })}></MyInput>
      <MyButton className="button1" onClick={AddNewProject}>Добавить</MyButton>
      </div>
      <div className="formversion right">
          <MyButton className="button1" style={{marginRight: "10px"}} onClick={selectedProject ? () => showForm(true) : () => {}}>Добавить</MyButton>
          <MyButton className="button1" style={{marginRight: "10px"}} onClick={selectedVersion ? () => showForm(false) : () => {}}>Изменить</MyButton>
          <MyButton className="button1" onClick={selectedProject && selectedVersion  ? () => DeleteVersion(selectedVersion) : () => {}}>Удалить</MyButton>
          </div>
      </div>
      

      <div className="left-section">
      <List remove={DeleteProject} Projects={Projects} ClearselectedVersion={setSelectedVersion} onSelectProject={setSelectedProject} onEditProject={EditProjectPreCheck}>Проекты</List>
      </div>

      

      <div className="right-section">
      {(selectedProject && selectedProject.versions && selectedProject.versions.length > 0) && (
        <VersionsList remove={DeleteVersion} versions={SortedVersions} selectedVersion={setSelectedVersion} >
          Версии
        </VersionsList>
      )}
      </div>
      
      {isFormVisible && (
        <div>
          {changeAddForm
            ? <FormVersion active={isFormVisible} SetActive={setFormVisible} onAddVersion={handleAddVersion} onCancel={hideForm}></FormVersion>
            : <FormVersion active={isFormVisible} SetActive={setFormVisible} onAddVersion={handleChangeVersionPreCheck} selectedVersion={selectedVersion} onCancel={hideForm}></FormVersion>}
        </div>
      )}
      
    </div>
    </div>
  );

}

export default Project;