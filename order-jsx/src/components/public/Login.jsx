import React from "react";

import { Formik, ErrorMessage } from "formik";

import { Link } from "react-router-dom";

import "./Login.dir/Login.css";

import { net } from "../../io/net";

import {
  Button,
  Loader,
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
        }
        
        return errors;
      }}
      onSubmit={(values, { setSubmitting }) => {
        setTimeout(async () => {
          try {
            const response = await net.login(values.email, values.password);
            console.log(response);
            setSubmitting(false);
          } catch (error) {
            console.log(error);
            setSubmitting(false);
          }
        }, 100);
      }}
    >
      {({
        values,
        errors,
        touched,
        handleChange,
        handleBlur,
        handleSubmit,
        isSubmitting,
        // isValid,
      }) => (
        <Form size="large" onSubmit={handleSubmit}>
          <Segment stacked>
            <ErrorMessage name="email" component="div" className="error-msg" />
            <Form.Input
              fluid
              icon="user"
              iconPosition="left"
              placeholder="E-mail address"
              name="email"
              onChange={handleChange}
              onBlur={handleBlur}
              value={values.email}
              error={errors.email && touched.email}
            />
            
            <ErrorMessage name="password" component="div" className="error-msg" />
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
              error={errors.password && touched.password}
            />
            
            { !isSubmitting && <>
              <button
              className="button-login-action"
              type="submit"
              >
                Login
              </button>
            </> }

            { isSubmitting && <>
              <Loader active inline size="small" />
            </> }
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
