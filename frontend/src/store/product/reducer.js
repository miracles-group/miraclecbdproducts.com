import actions from './actions';

const initialState = {
  listProduct: [],
  autoSyncProduct: false
};

const Product = (state = initialState, action) => {
  switch (action.type) {
    case actions.GET_PRODUCT_SUCCESS:
      return {
        ...state,
        listProduct: action.data
      };
    case actions.GET_SETTING_SUCCESS:
      return {
        ...state,
        autoSyncProduct: action.data
      };
    case actions.SETTING_SUCCESS:
      return {
        ...state,
        autoSyncProduct: action.data
      };
    default:
      return state;
  }
};

export default Product;
