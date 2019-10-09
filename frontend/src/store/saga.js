import { all } from 'redux-saga/effects';
import productSaga from './product/saga';
import authSaga from './auth/saga';

export default function* rootSaga() {
  yield all([productSaga(), authSaga()]);
}
