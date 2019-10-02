import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import './styles/index.scss';
import '../node_modules/noty/lib/noty.css';
import '../node_modules/noty/lib/themes/mint.css';
import 'utils/i18n';

ReactDOM.render(<App />, document.getElementById('root'));
