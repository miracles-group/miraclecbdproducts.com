import actions from './actions';

const initialState = {
  token: ''
};

const Auth = (state = initialState, action) => {
  switch (action.type) {
    case actions.LOGIN_SUCCESS:
      return {
        ...state,
        token: action.data
      };
    default:
      return state;
  }
};

export default Auth;
