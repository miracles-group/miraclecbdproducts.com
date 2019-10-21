import React from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import history from 'utils/history';
import { PrivateRoute } from './utils/PrivateRoute';
import Layout from './containers/layout';
import Login from './containers/auth/Login';
import SignUp from './containers/auth/SignUp';

const AppRouter = () => {
  return (
    <Router history={history}>
      <Switch>
        <PrivateRoute exact path="/" component={Layout}></PrivateRoute>
        <Route path="/login" component={Login}></Route>
        <Route path="/singup" component={SignUp}></Route>
      </Switch>
    </Router>
  );
};

export default AppRouter;
