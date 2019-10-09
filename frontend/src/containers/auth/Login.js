import React from 'react';
import Button from '@material-ui/core/Button';
import CssBaseline from '@material-ui/core/CssBaseline';
import TextField from '@material-ui/core/TextField';
import Link from '@material-ui/core/Link';
import Grid from '@material-ui/core/Grid';
import Typography from '@material-ui/core/Typography';
import Container from '@material-ui/core/Container';
import Noty from 'noty';
import history from '../../utils/history';
import { authActions } from '../../store/actions';
import { connect } from 'react-redux';

class Login extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      user: {
        username: '',
        password: ''
      },
      loading: false
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

  signIn = event => {
    event.preventDefault();
    this.props.sigin(this.state.user, this.onSuccess, this.onFail);
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
        <div className="form-wapper">
          <div className="avatar">
            <img
              className="logo"
              src="https://miraclecbdproducts.com/images/cbd-logo.png"
              width="120"
              alt="logo"
            />
          </div>
          <Typography component="h1" variant="h5">
            Sign in
          </Typography>
          <form className="form" noValidate onSubmit={this.signIn} method="POST">
            <TextField
              onChange={this.handleChange}
              variant="outlined"
              margin="normal"
              required
              fullWidth
              label="Username"
              name="username"
              autoComplete="email"
              autoFocus
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
            {/* <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Remember me"
            /> */}
            <Button type="submit" fullWidth variant="contained" color="primary" className="submit">
              Sign In
            </Button>
            <Grid container>
              <Grid item xs>
                <Link href="#" variant="body2">
                  Forgot password?
                </Link>
              </Grid>
              <Grid item>
                <Link href="#" variant="body2">
                  {"Don't have an account? Sign Up"}
                </Link>
              </Grid>
            </Grid>
          </form>
        </div>
      </Container>
    );
  }
}

const mapDispatchToProps = {
  sigin: authActions.login
};

export default connect(
  null,
  mapDispatchToProps
)(Login);
