import React from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import history from 'utils/history';
import { PrivateRoute } from './utils/PrivateRoute';
import Product from './containers/product';
import Login from './containers/auth/Login';
// import Layout from '../containers/layout';

const AppRouter = () => {
  return (
    <Router history={history}>
      <Switch>
        <PrivateRoute exact path="/" component={Product}></PrivateRoute>
        {/* <PrivateRoute path="/demo" component={() => <h1>asd</h1>}></PrivateRoute> */}
        <Route path="/login" component={Login}></Route>
      </Switch>
    </Router>
  );
};

export default AppRouter;
