import request from '../utils/request';
import axios from 'axios';

export const getProduct = () => {
  return request({
    url: `/product/?username=phamminh1309`,
    method: 'GET'
  });
};

export const createProduct = params => {
  return axios({
    url: 'https://shopify.miracles.vn/api/products',
    method: 'POST',
    data: params
  });
};
