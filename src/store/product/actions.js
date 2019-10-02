const actions = {
  GET_PRODUCT_REQUEST: 'GET_PRODUCT_REQUEST',
  GET_PRODUCT_SUCCESS: 'GET_PRODUCT_SUCCESS',

  getProduct: () => ({
    type: actions.GET_PRODUCT_REQUEST
  }),

  ADD_PRODUCT_REQUSET: 'ADD_PRODUCT_REQUSET',
  ADD_PRODUCT_SUCCESS: 'ADD_PRODUCT_SUCCESS',

  addProduct: params => ({
    type: actions.ADD_PRODUCT_REQUSET,
    params
  })
};

export default actions;
