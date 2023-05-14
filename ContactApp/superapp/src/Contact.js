import { Col, Image, ListGroupItem, Row } from "react-bootstrap";

function Contact(props) {
    return (
        <ListGroupItem>
            <Row>
                <Col className={"col-4"} >
                    <a href={`/contact/${props.contact.contactId}`} className={"mr-3"}>
                        <h3 className={"CONTACT-content"}>{props.contact.username}</h3>
                    </a>
                </Col>
                <Col>
                    {props.post.surname}
                </Col>
                <Col className={"col-4"}>
                    {props.contact.email}
                </Col>
            </Row>
        </ListGroupItem>
    )
}

export default Contact;