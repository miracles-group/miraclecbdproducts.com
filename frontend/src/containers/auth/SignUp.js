import React from 'react';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import PropTypes from 'prop-types';
import Noty from 'noty';
import history from '../../utils/history';
import { Link } from 'react-router-dom';
import { authActions } from '../../store/actions';
import { connect } from 'react-redux';

const PropType = {
  sigUp: PropTypes.func.isRequired
};

class SignUp extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      user: {
        username: '',
        password: '',
        emailaddress: '',
        phonenumber: '',
        contactperson: '',
        name: ''
      },
      re_pass: '',
      loading: false,
      valiEmail: false,
      valiPass: false
    };
  }

  handleChange = event => {
    const value = event.target.value;
    const name = event.target.name;
    this.setState({
      user: {
        ...this.state.user,
        [name]: value
      }
    });
  };

  valiEmail = event => {
    const regex = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
    if (!regex.test(event.target.value)) {
      this.setState({
        valiEmail: true
      });
    } else {
      this.setState({
        valiEmail: false
      });
    }
  };

  onValiPass = () => {
    if (this.state.re_pass !== this.state.user.password) {
      this.setState({
        valiPass: true
      });
    } else {
      this.setState({
        valiPass: false
      });
    }
  };

  signIn = event => {
    event.preventDefault();
    if (this.state.valiEmail || this.state.valiPass) {
      return;
    }
    this.props.sigUp(this.state.user, this.onSuccess, this.onFail);
  };
  onSuccess = () => {
    this.setState(
      {
        loading: false
      },
      () => history.push('/')
    );
  };
  onFail = err => {
    this.setState(
      {
        loading: false
      },
      () => {
        return new Noty({
          type: 'error',
          layout: 'topRight',
          text: err,
          timeout: 3000
        }).show();
      }
    );
  };

  render() {
    return (
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <div className="form-wapper form-register">
          <div className="avatar">
            <img
              className="logo"
              src="https://miraclecbdproducts.com/images/cbd-logo.png"
              width="120"
              alt="logo"
            />
          </div>
          <Typography component="h1" variant="h5">
            SignUp
          </Typography>
          <form className="form" onSubmit={this.signIn} method="POST">
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="name"
              label="Name"
              autoFocus
            />
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              error={this.state.valiEmail}
              onBlur={this.valiEmail}
              fullWidth
              name="emailaddress"
              label="Email"
              type="mail"
              autoComplete="current-password"
            />
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="phonenumber"
              label="Phone Number"
              type="number"
              autoComplete="current-password"
            />
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="contactperson"
              label="Company"
              type="text"
            />
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              label="Miracle Username"
              name="username"
            />
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Password"
              type="password"
              autoComplete="current-password"
            />

            <TextField
              onChange={e => this.setState({ re_pass: e.target.value })}
              error={this.state.valiPass}
              onBlur={this.onValiPass}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              name="password"
              label="Comfirm Password"
              type="password"
              autoComplete="current-password"
            />

            <Button type="submit" fullWidth variant="contained" color="primary" className="submit">
              Sign Up
            </Button>
            <Link to="/login">{'Already a member ? Login'}</Link>
          </form>
        </div>
      </Container>
    );
  }
}

const mapDispatchToProps = {
  sigUp: authActions.sigUp
};

SignUp.propTypes = PropType;

export default connect(
  null,
  mapDispatchToProps
)(SignUp);
