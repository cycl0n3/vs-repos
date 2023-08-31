import React from "react";

import { Formik, ErrorMessage } from "formik";

import { Link } from "react-router-dom";

import "./Login.dir/Login.css";

import {
  Button,
  Form,
  Grid,
  Header,
  Image,
  Message,
  Segment,
} from "semantic-ui-react";

const LoginForm = () => {
  return (
    <Formik
      initialValues={{ email: "", password: "" }}
      validate={(values) => {
        const errors = {};

        if (!values.email) {
          errors.email = "Required";
        } else if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i.test(values.email)) {
          errors.email = "Invalid email address";
        } else if (values.email.length > 50) {
          errors.email = "Email must be less than 50 characters";
        } else if (values.email.length < 5) {
          errors.email = "Email must be at least 5 characters";
        }

        if (!values.password) {
          errors.password = "Required";
        } else if (values.password.length < 4) {
          errors.password = "Password must be at least 4 characters";
        } else if (values.password.length > 10) {
          errors.password = "Password must be less than 10 characters";
        } else if (!/^[a-zA-Z0-9]+$/i.test(values.password)) {
          errors.password = "Password must contain only alphanumeric characters";
        } else if (!/\d/.test(values.password)) {
          errors.password = "Password must contain at least one number";
        } else if (!/[a-zA-Z]/i.test(values.password)) {
          errors.password = "Password must contain at least one letter";
        } else if (!/[!@#$%^&*]/i.test(values.password)) {
          errors.password = "Password must contain at least one special character";
        } else if (values.password !== values.confirmPassword) {
          errors.password = "Password and Confirm Password must match";
        }
        
        return errors;
      }}
      onSubmit={(values, { setSubmitting }) => {
        setTimeout(() => {
          alert(JSON.stringify(values, null, 2));

          setSubmitting(false);
        }, 400);
      }}
    >
      {({
        values,
        handleChange,
        handleBlur,
        handleSubmit,
        isSubmitting,
        /* and other goodies */
      }) => (
        <Form size="large" onSubmit={handleSubmit}>
          <Segment stacked>
            <Form.Input
              fluid
              icon="user"
              iconPosition="left"
              placeholder="E-mail address"
              name="email"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.email}
            />
            <ErrorMessage name="email" component="div" />

            <Form.Input
              fluid
              icon="lock"
              iconPosition="left"
              placeholder="Password"
              type="password"
              name="password"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.password}
            />
            <ErrorMessage name="password" component="div" />

            <button
              className="button-login-action"
              type="submit"
              disabled={isSubmitting}
            >
              Login
            </button>
          </Segment>
        </Form>
      )}
    </Formik>
  );
};

const Login = () => (
  <>
    <Grid textAlign="center" style={{ height: "100vh" }} verticalAlign="middle">
      <Grid.Column style={{ maxWidth: 450 }}>
        <Header as="h2" color="teal" textAlign="center">
          Log-In to your account
        </Header>
        <LoginForm />
        <Message>
          New to us? <Link to="/signup">Sign Up</Link>
        </Message>
      </Grid.Column>
    </Grid>
  </>
);

export default Login;
