import actions from './actions';
import { all, fork, put, takeLatest, takeEvery } from 'redux-saga/effects';
import { getProduct, createProduct } from '../../services/products';

export function* getProductSaga() {
  yield takeLatest(actions.GET_PRODUCT_REQUEST, function*() {
    try {
      const res = yield getProduct();
      if (res.status === 200) {
        yield put({ type: actions.GET_PRODUCT_SUCCESS, data: res.data.results });
      }
    } catch (error) {
      console.log(error);
    }
  });
}

export function* createProductSaga() {
  yield takeEvery(actions.ADD_PRODUCT_REQUSET, function*(data) {
    try {
      const res = yield createProduct(data.params);
      console.log(res);
    } catch (error) {
      alert(error);
    }
  });
}

export default function* rootSaga() {
  yield all([fork(getProductSaga), fork(createProductSaga)]);
}
