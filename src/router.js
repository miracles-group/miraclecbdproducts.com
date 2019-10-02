import React from 'react';
import { Router, Switch, Route } from 'react-router-dom';
import history from 'utils/history';
import Product from './containers/product';
import Login from './containers/auth/Login';
import Layout from './containers/layout';

const AppRouter = () => {
  return (
    <Router history={history}>
      <Layout>
        <Switch>
          <Route exact path="/" component={Product}></Route>
          <Route path="/login" component={Login}></Route>
        </Switch>
      </Layout>
    </Router>
  );
};

export default AppRouter;
