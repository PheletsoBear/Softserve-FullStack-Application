import React, { Fragment, useEffect, useState } from 'react'
import axios from 'axios';
import Table from 'react-bootstrap/Table';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Container from 'react-bootstrap/Container';
import { Toast } from 'bootstrap';
import { ToastContainer, toast } from 'react-toastify';

const CustomerCRUD = () => {
//Handle Edit
  const [show, setShow] = useState(false);
  
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
 

  //Handle delete alert
  const [show2, setShow2] = useState(false);
  
  const handleClose2 = () => setShow2(false);
  const handleShow2 = () => setShow2(true);

// For submit

const [firstName, setfirstName] = useState ('');
const [LastName, setLastName] = useState ('');
const [UserName, setUserName] = useState ('');
const [EmailAddress, setEmailAddress] = useState ('');
const [DateOfBirth, setDateOfBirth] = useState ('');
const [Age, setAge] = useState ('');
const [DateCreated, setDateCreated] = useState ('');
const [DateEdited, setDateEdited] = useState ('');
const [IsDeleted, setIsDeleted] = useState (0);


  
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
  axios.get('https://localhost:7066/api/Customer').then((result) =>{
    console.log(result.data);

    
  
  }).catch((error)=>{

    console.error('Error fetching data:', error); 
    });

  
}  


const HandleActiveEditChange = (e) =>{
  if(e.target.checked){
    setEditIsDeleted(1);
  }
  else{
    setEditIsDeleted(0);
  }
}

const HandleFocusDate =(e) =>{
  e.target.type = 'Date';
}
const HandleFocuDatetime =(e) =>{
  e.target.type = 'datetime-local';
}

const HandleDelete = (id) =>{
  //this displays alert upon window confirmation
  handleShow2();
  console.log(id)
 } 

 
 const HandleSave = ()=>{
  const url = 'https://localhost:7066/api/Customer';
  const data =	 {
    
    "firstName": firstName,
    "age": Age,
    "lastName": LastName,
    "userName" : UserName,
     "emailAddress" : EmailAddress,
     "dateOfBirth" : DateOfBirth,
     "dateCreated": DateCreated,
     "dateEdited" : DateEdited,
     "isDeleted" : IsDeleted

  
  }

  axios.post(url, data).then((result)=>{
    GetData();
    clear();
    toast.success('Employee has been Added');
  })

}

const clear = ( ) =>{
  setfirstName ('');
  setLastName('');
  setUserName('');
  setEmailAddress('');
  setDateOfBirth('');
  setAge('');
  setDateEdited('');
  setDateCreated('');
  setIsDeleted(0);
}


  return (
    <div>

<Fragment>
<Container>
      <br/>
      <Row>
        <Col>
        <input type='text' className='form-control' placeholder='Enter firstName ' value={firstName} 
        onChange={(e) => setfirstName(e.target.value)}
        />
        <br />
        </Col>
        <Col>
         <input type='text' className='form-control' placeholder='Enter LastName' value={LastName}
          onChange={(e) => setLastName(e.target.value)} />
        <br />
        </Col>
        </Row>
        <Row>

        
        <Col>
         <input type='text' className='form-control' placeholder='Enter UserName' value={UserName}
          onChange={(e) => setUserName(e.target.value)} />
        <br />
        </Col>
        
        
        <Col>
         <input type='email' className='form-control' placeholder='Enter Email Address' value={EmailAddress}
          onChange={(e) => setEmailAddress(e.target.value)} />
        <br />
        </Col>
       </Row>
       <Row> 
        <Col>
         <input type='text' className='form-control' placeholder='Enter Date of birth' value={DateOfBirth}
          onChange={(e) => setDateOfBirth(e.target.value)} onFocus={HandleFocusDate}/>
        <br />
        </Col>
        <Col>
         <input type='number' className='form-control' placeholder='Age ' value={Age}
          onChange={(e) => setAge(e.target.value)} />
        <br />
        </Col>
        
      </Row>
      <Row>

      <Col className="date-input-container">
         <input type='text' className='form-control' placeholder='Enter Date Created'  value={DateCreated}
          onChange={(e) => setDateCreated(e.target.value)}  onFocus={HandleFocuDatetime} />
        <br />
        </Col>
        <Col>
         <input type='text' className='form-control' placeholder='Enter Date Edited ' value={DateEdited}
          onChange={(e) => setDateEdited(e.target.value)} onFocus={HandleFocuDatetime} />
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

        <Col>
        <Button  className='btn btn-primary' onClick={()=>HandleSave()}>submit</Button>
        </Col>
       

      </Row>
      
    </Container>


    
      <br/>
      <h1>Customers</h1>
      <br/>
    <Table striped bordered hover>
    <thead>
      <tr>
        <th>#</th>
        <th>first Name</th>
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
        <button className='btn btn-danger' onClick = {()=>HandleDelete(item.customerID )}>Delete</button>
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
         <input type='text' className='form-control' placeholder='Enter Date of birth' value={editDateOfBirth}
          onChange={(e) => seteditDateOfBirth(e.target.value)} onFocus={HandleFocusDate}/>
        <br />
        </Col>
        <Col>
         <input type='number' className='form-control' placeholder='Age ' value={editAge}
          onChange={(e) => setEditeditAge(e.target.value)} />
        <br />
        </Col>
        
      </Row>
      <Row>

      <Col className="date-input-container">
         <input type='text' className='form-control' placeholder='Enter Date Created'  value={editDateCreated}
          onChange={(e) => setEditDateCreated(e.target.value)}  onFocus={HandleFocuDatetime} />
        <br />
        </Col>
        <Col>
         <input type='text' className='form-control' placeholder='Enter Date Edited ' value={editDateEdited}
          onChange={(e) => setEditDateEdited(e.target.value)} onFocus={HandleFocuDatetime} />
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

    
      {/*Delete modal */}

      
      <Modal
        show={show2}
        onHide={handleClose2}
        backdrop="static"
        keyboard={false}
      >
        <Modal.Header closeButton>
          <Modal.Title>Delete User</Modal.Title>
        </Modal.Header>
        <Modal.Body>
         <p >
          Are you sure?
         </p>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose2}>
            Cancel
          </Button>
          <Button variant="danger">Delete</Button>
        </Modal.Footer>
      </Modal>

    </Fragment>
</div>
   )}
export default CustomerCRUD
