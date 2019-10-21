const actions = {
  LOGIN_REQUEST: 'LOGIN_REQUEST',
  LOGIN_SUCCESS: 'LOGIN_SUCCESS',

  login: (params, success, fail) => ({
    type: actions.LOGIN_REQUEST,
    params,
    success,
    fail
  }),

  SINGUP_REQUEST: 'SINGUP_REQUEST',
  SINGUP_SUCCESS: 'SINGUP_SUCCESS',

  sigUp: (params, success, fail) => ({
    type: actions.SINGUP_REQUEST,
    params,
    success,
    fail
  })
};

export default actions;
