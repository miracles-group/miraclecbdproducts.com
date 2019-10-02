import actions from './actions';

const initialState = {
  listProduct: []
};

const Product = (state = initialState, action) => {
  switch (action.type) {
    case actions.GET_PRODUCT_SUCCESS:
      return {
        ...state,
        listProduct: action.data
      };
    default:
      return state;
  }
};

export default Product;
