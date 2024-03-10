import React, { Component } from 'react';
import "./style.css";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <div className="menu js-menu">
        <ul className="menu-list js-menu-list">
          <li className="menu-item js-menu-item">
            <a className='link' href='preparation'>Подготовка</a>
          </li>
          <li className="menu-item js-menu-item">
            <a className='link' href='experiment'>Эксперимент</a>
          </li>
          <li className="menu-item js-menu-item">
            <a className='link' href='/expertAssessment'>Экспертное оценивание</a>
          </li>
          <li className="menu-item js-menu-item">
            <a className="link" href='/analysis'>Анализ</a>
          </li>
        </ul>
        </div>
      </div>
    );
  }
}
