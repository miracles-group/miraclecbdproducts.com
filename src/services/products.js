import request from '../utils/request';
import axios from 'axios';

export const getProduct = () => {
  return request({
    url: `/product/?username=phamminh1309`,
    method: 'GET'
  });
};

export const createProduct = params => {
  //   const config = {
  //     auth: {
  //       username: 'd97bf09f0f651455af9eef6155325dd2',
  //       password: 'da2156d2b9d65ee66c76b15bf697a90'
  //     }
  //   };

  return axios({
    url: 'https://miracles1310.myshopify.com/admin/api/2019-07/products.json',
    method: 'POST',
    withCredentials: true,
    auth: {
      username: 'd97bf09f0f651455af9eef6155325dd2',
      password: 'da2156d2b9d65ee66c76b15bf697a90f'
    },
    // headers: {
    //   Authorization:
    //     'Basic ZDk3YmYwOWYwZjY1MTQ1NWFmOWVlZjYxNTUzMjVkZDI6ZGEyMTU2ZDJiOWQ2NWVlNjZjNzZiMTViZjY5N2E5MGY='
    // },
    data: params
  });
  // return axios.post('http://relist.at/api/auth/login', { username: 'vanh', password: '123' });
};

export const test = () => {
  return axios.post('http://relist.at/api', { username: 'vanh', password: '123' });
};
