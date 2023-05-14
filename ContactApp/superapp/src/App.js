import axios from "axios";
import logo from './logo.svg';
import './App.css';
import Contact from "./Contact";
import { Button, Col, Container, Form, ListGroup, ListGroupItem, Modal, Row } from "react-bootstrap";
import { useEffect, useState } from "react";


function App(props) {
    const [contacts, setContacts] = useState([]);
    const [email, setEmail] = useState();
    const [password, setPassword] = useState();

    function login(e) {
        e.preventDefault();
        axios.post('https://localhost:7069/api/Auth/login', {
            email: email,
            password: password
        }).then(response => {
            const token = response.data.token;
            localStorage.setItem('accessToken', token);
            // redirect or perform other actions
        }).catch(err => {
            console.log(err.response);
        });
    }

    const accessToken = localStorage.getItem('accessToken');

    useEffect(() => {
        axios.get(`https://localhost:7069/api/Auth/getAllUsers`, {
            headers: {
                Authorization: `Bearer ${accessToken}`
            }
        })
            .then(response => {
                setContacts(response.data)
            }).catch(err => {
                console.log(err.response)
            })
    }, [])

  return (
      <Container className={"Contacts-container"}>
          <Row className={"mt-4"}>
              <Col className={"col-4"}>
                  <h4>
                  Name
                  </h4>
              </Col>
              <Col className={"col-4"}>
                  <h4>
                      Email
                  </h4>
              </Col>
          </Row>
          <Row>
              {contacts.length > 0 ?
                  <ListGroup className={"list-group"}>
                      {
                          contacts.map((contact, key) =>
                              <ListGroupItem key={key}>
                                  <Contact contact={contact} />
                              </ListGroupItem>
                          )
                      }
                  </ListGroup>
                  :
                  <h3 className={"mt-3"}>No contacts found</h3>
              }
          </Row>
      </Container>
  );
}

export default App;
