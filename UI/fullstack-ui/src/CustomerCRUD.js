import React, { Fragment, useEffect, useState } from 'react'
import axios from 'axios';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';

const CustomerCRUD = () => {

  const [show, setShow] = useState(false);
  
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

//For Update
const [editCustomerID, setEditCustomerID] = useState ('');
const [editfirstName, seteditfirstName] = useState ('');
const [editLastName, setEditeditLastName] = useState ('');
const [editUserName, seteditUserName] = useState ('');
const [editEmailAddress, setEditEmailAddress] = useState ('');
const [editDateOfBirth, seteditDateOfBirth] = useState ('');
const [editAge, setEditeditAge] = useState ('');
const [editDateCreated, setEditDateCreated] = useState ('');
const [editDateEdited, setEditDateEdited] = useState ('');
const [editIsDeleted, setEditIsDeleted] = useState (0);


const [data, setData] = useState([]);

useEffect(() =>{
   
  GetData();
   
},[]);

const GetData = () =>{

  axios.get('https://localhost:7066/api/Customer').then((result) =>{
    console.log(result.data);

    setData(result.data)
  
  }).catch((error)=>{

    console.error('Error fetching data:', error); 
    });

}
  
// hANDLE update

const handleUpdate = ()=>{

} 

// handle Button Mehod

const HandleEdit = (customerID) =>{
  //alert(id);
  handleShow();
  console.log(customerID)
}  


const HandleActiveEditChange = (e) =>{
  if(e.target.checked){
    setEditIsDeleted(1);
  }
  else{
    setEditIsDeleted(0);
  }
}


  return (
    <div>
   
      <br/>
    <Fragment>
    <Table striped bordered hover>
    <thead>
      <tr>
        <th>#</th>
        <th>firstName</th>
        <th>Last Name</th>
        <th>UserName</th>
        <th>Email Address</th>
        <th>Date of birth</th>
        <th>Age</th>
        <th>Date Created</th>
        <th>Date Edited</th>
        <th>IsDeleted?</th>


      </tr>
    </thead>

    <tbody>

      
    {
  /* The below code is a representation of the ternary operator which conditionally renders the table body based on whether the data variable exists and if the length is non-zero. If the data exists, data is rendered, and if not, a "Loading" message is displayed. */
}
{data && data.length ? ( // Use parentheses for clearer structure

  // Mapping data array to JSX elements
  data.map((item, index) => (
    <tr key={index}>
      <td>{index + 1}</td>
      
      <td>{item.firstName}</td>
      <td>{item.lastName}</td>
      <td>{item.userName}</td>
       <td>{item.emailAddress}</td>
      <td>{item.dateOfBirth}</td>
       <td>{item.age}</td>
       <td>{item.dateCreated}</td>
       <td>{item.dateEdited}</td>
       <td>{item.isDeleted ? 'true' : 'false'}</td>

      <td colSpan={2}>
        <button className='btn btn-primary' onClick = {()=>HandleEdit(item.customerID)}>Edit</button> &nbsp;
        <button className='btn btn-danger'>Delete</button>
      </td>
    </tr>
  ))

) : (
  <tr>
    <td colSpan={6}>No Data!!</td>
  </tr>
)}


 </tbody>

    </Table>

    <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Update Customer</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <Container>
      <br/>
      <Row>
        <Col>
        <input type='text' className='form-control' placeholder='Enter firstName ' value={editfirstName} 
        onChange={(e) => seteditfirstName(e.target.value)}
        />
        <br />
        </Col>
        <Col>
         <input type='text' className='form-control' placeholder='Enter LastName' value={editLastName}
          onChange={(e) => setEditeditLastName(e.target.value)} />
        <br />
        </Col>
        </Row>
        <Row>

        
        <Col>
         <input type='text' className='form-control' placeholder='Enter UserName' value={editUserName}
          onChange={(e) => seteditUserName(e.target.value)} />
        <br />
        </Col>
        
        
        <Col>
         <input type='email' className='form-control' placeholder='Enter Email Address' value={editEmailAddress}
          onChange={(e) => setEditEmailAddress(e.target.value)} />
        <br />
        </Col>
       </Row>
       <Row> 
        <Col>
         <input type='date' className='form-control' placeholder='Enter Date of birth' value={editDateOfBirth}
          onChange={(e) => seteditDateOfBirth(e.target.value)} />
        <br />
        </Col>
        <Col>
         <input type='number' className='form-control' placeholder='Age ' value={editAge}
          onChange={(e) => setEditeditAge(e.target.value)} />
        <br />
        </Col>
        
      </Row>
      <Row>

      <Col>
         <input type='date' className='form-control' placeholder='Date Created'  value={editDateCreated}
          onChange={(e) => setEditDateCreated(e.target.value)}   />
        <br />
        </Col>
        <Col>
         <input type='date' className='form-control' placeholder='Date Edited ' value={editDateEdited}
          onChange={(e) => setEditDateEdited(e.target.value)} />
        <br />
        </Col>

      </Row>
      <Row>
      <Col>
      <input type='checkbox' 
       checked = {editIsDeleted === 1? true: false} 
       onChange={(e)=> HandleActiveEditChange(e)} value ={editIsDeleted} />&nbsp;
        <label>IsDeleted</label>
        
        </Col>

      </Row>
      
    </Container>


        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleUpdate}>
            Update
          </Button>
        </Modal.Footer>
      </Modal>

    
    </Fragment>
</div>
   )}
export default CustomerCRUD