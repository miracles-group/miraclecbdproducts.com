import actions from './actions';
import { all, fork, put, takeLatest, takeEvery } from 'redux-saga/effects';
import { getProduct, createProduct, getSetting, setting } from '../../services/products';
import Noty from 'noty';

export function* getProductSaga() {
  yield takeLatest(actions.GET_PRODUCT_REQUEST, function*(data) {
    try {
      const res = yield getProduct();

      if (res.status === 200) {
        yield put({ type: actions.GET_PRODUCT_SUCCESS, data: res.data.results });
        yield data.onload();
      }
    } catch (error) {
      yield new Noty({
        type: 'error',
        layout: 'topRight',
        text: 'adsasd',
        timeout: 3000
      }).show();
    }
  });
}

export function* createProductSaga() {
  yield takeEvery(actions.ADD_PRODUCT_REQUSET, function*(data) {
    try {
      const res = yield createProduct(data.params);
      if (res.data.status === 200) {
        yield data.success();
      } else {
        yield data.fail(res.data.message);
      }
    } catch (error) {
      yield data.fail(error);
    }
  });
}

export function* getSettingSaga() {
  yield takeLatest(actions.GET_PRODUCT_REQUEST, function*() {
    try {
      const res = yield getSetting();
      yield put({ type: actions.GET_SETTING_SUCCESS, data: res.data.autoSyncProduct });
    } catch (error) {
      yield new Noty({
        type: 'error',
        layout: 'topRight',
        text: error,
        timeout: 3000
      }).show();
    }
  });
}

export function* settingSaga() {
  yield takeEvery(actions.SETTING_REQUEST, function*(data) {
    try {
      const res = yield setting(data.params);
      if (res.data.status === 200) {
        yield put({ type: actions.SETTING_SUCCESS, data: res.data.data.autoSyncProduct });
        yield data.success();
      } else {
        yield data.fail(res.data.message);
      }
    } catch (error) {
      yield data.fail(error);
    }
  });
}

export default function* rootSaga() {
  yield all([
    fork(getProductSaga),
    fork(createProductSaga),
    fork(getSettingSaga),
    fork(settingSaga)
  ]);
}
