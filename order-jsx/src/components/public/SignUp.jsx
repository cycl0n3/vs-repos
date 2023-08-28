import React from "react";

import { Link } from "react-router-dom";

import {
  Button,
  Form,
  Grid,
  Header,
  Image,
  Message,
  Segment,
} from "semantic-ui-react";

const SignUp = () => (
  <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
    <Grid.Column style={{ maxWidth: 450 }}>
      <Header as="h2" color="teal" textAlign="center">
        <Image src="/logo.png" /> Sign-Up new account
      </Header>
      <Form size="large">
        <Segment stacked>
          <Form.Input
            fluid
            icon="user"
            iconPosition="left"
            placeholder="E-mail address"
          />

          <Form.Input
            fluid
            icon="lock"
            iconPosition="left"
            placeholder="Password"
            type="password"
          />

          <Form.Input
            fluid
            icon="lock"
            iconPosition="left"
            placeholder="Confirm Password"
            type="password"
          />

          <Button color="teal" fluid size="large">
            Sign Up
          </Button>
        </Segment>
      </Form>
      <Message>
        Existing user? <Link to="/login">Login</Link>
      </Message>
    </Grid.Column>
  </Grid>
);

export default SignUp;
