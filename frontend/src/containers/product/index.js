import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import TablePagination from '@material-ui/core/TablePagination';
import Paper from '@material-ui/core/Paper';
import { Sync } from '@material-ui/icons';
import { productActions } from '../../store/actions';
import { connect } from 'react-redux';

class Product extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      page: 0,
      rowsPerPage: 20
    };
  }

  componentDidMount() {
    this.props.getProduct();
  }

  handleChangePage = (event, newPage) => {
    this.setState({
      page: newPage
    });
  };

  handleChangeRowsPerPage = event => {
    this.setState({
      rowsPerPage: +event.target.value,
      page: 0
    });
  };

  addProduct = product => {
    const item = {
      title: product.name,
      body_html: product.description,
      vendor: product.slug,
      product_type: product.category,
      tags: product.sub_category,
      variants: [
        {
          price: product.price,
          sku: product.mg_of_cbd
        }
      ],
      images: [
        {
          src: product.image_path
        }
      ]
    };
    this.props.addProduct(item);
  };

  render() {
    const { page, rowsPerPage } = this.state;
    return (
      <Paper>
        <Table>
          <TableHead>
            <TableRow>
              <TableCell align="left">#</TableCell>
              <TableCell align="left">Name</TableCell>
              <TableCell align="left">Category</TableCell>
              <TableCell align="left">Price</TableCell>
              <TableCell align="left">Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {this.props.product
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((value, index) => {
                return (
                  <TableRow key={index}>
                    <TableCell>{index + 1}</TableCell>
                    <TableCell align="left">{value.name}</TableCell>
                    <TableCell align="left">{value.sub_category}</TableCell>
                    <TableCell align="left">{value.price}</TableCell>
                    <TableCell value={value}>
                      <Sync className="add" onClick={() => this.addProduct(value)}></Sync>
                    </TableCell>
                  </TableRow>
                );
              })}
          </TableBody>
        </Table>
        <TablePagination
          component="div"
          rowsPerPageOptions={[10, 20, 30]}
          count={this.props.product.length}
          rowsPerPage={this.state.rowsPerPage}
          page={this.state.page}
          backIconButtonProps={{
            'aria-label': 'previous page'
          }}
          nextIconButtonProps={{
            'aria-label': 'next page'
          }}
          onChangePage={this.handleChangePage}
          onChangeRowsPerPage={this.handleChangeRowsPerPage}
        />
      </Paper>
    );
  }
}

const mapStateToProps = state => {
  return {
    product: state.product.listProduct
  };
};

const mapDispatchToProps = {
  getProduct: productActions.getProduct,
  addProduct: productActions.addProduct
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Product);
