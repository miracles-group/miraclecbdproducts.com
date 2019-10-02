import actions from './actions';
import { all, fork, put, takeLatest, takeEvery } from 'redux-saga/effects';
import { getProduct, createProduct } from '../../services/products';
import Noty from 'noty';

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
      if (res.data.status === 200) {
        new Noty({
          type: 'success',
          layout: 'topRight',
          text: 'Sync Successful',
          timeout: 3000
        }).show();
      } else {
        new Noty({
          type: 'error',
          layout: 'topRight',
          text: res.data.message,
          timeout: 3000
        }).show();
      }
    } catch (error) {
      new Noty({
        type: 'error',
        layout: 'topRight',
        text: error,
        timeout: 3000
      }).show();
    }
  });
}

export default function* rootSaga() {
  yield all([fork(getProductSaga), fork(createProductSaga)]);
}
