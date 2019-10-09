import React from 'react';
import { Route, Redirect } from 'react-router-dom';
import Layout from '../containers/layout';

export const PrivateRoute = ({ component: Component, ...rest }) => {
  return (
    <Route
      {...rest}
      render={props =>
        localStorage.getItem('logged') === 'true' ? (
          <Layout>
            <Component {...props} />
          </Layout>
        ) : (
          //   <Component {...props} />
          <Redirect to={{ pathname: '/login', state: { from: props.location } }} />
        )
      }
    />
  );
};
