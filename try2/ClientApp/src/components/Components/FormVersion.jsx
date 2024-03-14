import React, { useState, useEffect } from 'react';
import MyButton from './UI/MyButton';
import MyInput from './UI/MyInput';
import MyModal from './UI/MyModal/MyModal';

const FormVersion = ({ active, SetActive, onAddVersion, onCancel, selectedVersion, ...props }) => {

  const [NewVersion, setNewVersion] = useState({
    n: '',
    nn: '',
    nnn: '',
    descr: '',
  });

  useEffect(() => {
    console.log(selectedVersion);
    if (selectedVersion !== null && selectedVersion !== undefined) {
      setNewVersion(selectedVersion);
    }
  }, [selectedVersion]);


  const AddNewVersion = (e) => {
    e.preventDefault();
    onAddVersion(NewVersion);
    setNewVersion({ n: '', nn: '', nnn: '', descr: '' });
  };

//<MyButton onClick={selectedVersion ? (e) => AddNewVersion(e) : (e) => EditVersion(e)}>Ок</MyButton>

  return (
    <div>
      <MyModal active={active} setActive={SetActive}>
      <MyInput value={NewVersion.n} onChange={(e) => setNewVersion({...NewVersion, n: e.target.value})}>Версия проекта: </MyInput>
      <MyInput value={NewVersion.nn} onChange={(e) => setNewVersion({...NewVersion, nn: e.target.value})}>Подверсия проекта: </MyInput>
      <MyInput value={NewVersion.nnn} onChange={(e) => setNewVersion({...NewVersion, nnn: e.target.value})}>Номер сборки: </MyInput>
      <MyInput value={NewVersion.descr} placeholder={"Введите описание особенностей версии кабины"} style={{height: "100px", width: "250px"}} onChange={(e) => setNewVersion({...NewVersion, descr: e.target.value})}></MyInput>
      <MyButton onClick={AddNewVersion}>Ок</MyButton>
      </MyModal>
    </div>
  );
};

export default FormVersion;