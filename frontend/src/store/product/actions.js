const actions = {
  GET_PRODUCT_REQUEST: 'GET_PRODUCT_REQUEST',
  GET_PRODUCT_SUCCESS: 'GET_PRODUCT_SUCCESS',

  getProduct: onload => ({
    type: actions.GET_PRODUCT_REQUEST,
    onload
  }),

  ADD_PRODUCT_REQUSET: 'ADD_PRODUCT_REQUSET',
  ADD_PRODUCT_SUCCESS: 'ADD_PRODUCT_SUCCESS',

  addProduct: (params, success, fail) => ({
    type: actions.ADD_PRODUCT_REQUSET,
    params,
    success,
    fail
  }),

  GET_SETTING_REQUEST: 'GET_SETTING_REQUEST',
  GET_SETTING_SUCCESS: 'GET_SETTING_SUCCESS',

  getSetting: () => ({
    type: actions.GET_SETTING_REQUEST
  }),

  SETTING_REQUEST: 'SETTING_REQUEST',
  SETTING_SUCCESS: 'SETTING_SUCCESS',

  setting: (params, success, fail) => ({
    type: actions.SETTING_REQUEST,
    params,
    success,
    fail
  })
};

export default actions;
