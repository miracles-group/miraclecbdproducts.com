import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import AppRouter from './router';
import { createStore, applyMiddleware } from 'redux';
import { Provider } from 'react-redux';
import createSagaMiddleware from 'redux-saga';

import reducers from './store/reducers';
import saga from './store/saga';

const sagaMiddleware = createSagaMiddleware();
const store = createStore(reducers, applyMiddleware(sagaMiddleware));
sagaMiddleware.run(saga);

function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <AppRouter></AppRouter>
      </BrowserRouter>
    </Provider>
  );
}

export default App;
