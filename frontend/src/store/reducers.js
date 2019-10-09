import { combineReducers } from 'redux';
import product from './product/reducer';
import auth from './auth/reducer';

const reducer = combineReducers({
  product,
  auth
});

export default reducer;
